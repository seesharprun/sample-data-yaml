HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<IDataConverterService, DataConverterService>();
builder.Services.AddTransient<IFileParserService, FileParserService>();
builder.Services.AddTransient<IOutputGeneratorService, OutputGeneratorService>();

builder.Services.AddHostedService<ApplicationWorker>();

Argument<DirectoryInfo> inputDirectoryArgument = new(
    name: "input-directory",
    description: "The path to the file or directory to scan."
);

Option<FileInfo> outputFileOption = new(
    name: "--output-file",
    description: "The path to the output file.",
    getDefaultValue: () => new FileInfo("output.json")
)
{
    IsRequired = true,
    Arity = ArgumentArity.ExactlyOne
};

Option<Format> outputFormatOption = new(
    name: "--output-format",
    description: "The format of the output file.",
    getDefaultValue: () => Format.Json
)
{
    IsRequired = true,
    Arity = ArgumentArity.ExactlyOne
};

RootCommand command =
[
    inputDirectoryArgument,
    outputFileOption,
    outputFormatOption
];

command.SetHandler(async (inputDirectory, outputFile, outputFormat) =>
{
    Configuration configuration = new()
    {
        InputDirectory = inputDirectory,
        OutputFile = outputFile,
        OutputFormat = outputFormat
    };
    builder.Services.AddSingleton<Configuration>(configuration);

    IHost host = builder.Build();

    await host.RunAsync();
}, inputDirectoryArgument, outputFileOption, outputFormatOption);

await command.InvokeAsync(args);