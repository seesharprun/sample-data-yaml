namespace Examples.SampleData.Services;

/// <inheritdoc />
public sealed class OutputGeneratorService(
    ILogger<OutputGeneratorService> logger
) : IOutputGeneratorService
{
    /// <inheritdoc />
    public async Task GenerateOutputFileAsync<T>(Format format, FileInfo file, IAsyncEnumerable<T> data)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(file);

        List<T> items = [];
        await foreach(T item in data)
        {
            items.Add(item);
        }

        Action serializerAction = format switch
        {
            Format.Yaml => () => SerializeToYaml(items, file),
            Format.Json => async () => await SerializeToJsonAsync(items, file),
            Format.Xml => () => SerializeToXml(items, file),
            Format.Csv => async () => await SerializeToCsv(items, file),
            _ => throw new NotSupportedException($"Format '{format}' is not supported.")
        };

        serializerAction();

        logger.LogGeneratingOutput(format, file);
    }

    private static void SerializeToYaml<T>(List<T> items, FileInfo file)
    {
        ISerializer serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        using StreamWriter writer = file.OpenNewFileStreamWriter();
        serializer.Serialize(writer, items);
    }

    private static async Task SerializeToJsonAsync<T>(List<T> items, FileInfo file)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        using FileStream stream = file.OpenNewFileStream();
        await JsonSerializer.SerializeAsync(stream, items, options);
    }

    private static void SerializeToXml<T>(List<T> items, FileInfo file)
    {
        string rootName = typeof(T).Name.Pluralize();
        XmlSerializer serializer = new(typeof(List<T>), new XmlRootAttribute(rootName));

        using StreamWriter writer = file.OpenNewFileStreamWriter();
        serializer.Serialize(writer, items);
    }

    private static async Task SerializeToCsv<T>(List<T> items, FileInfo file)
    {
        CsvConfiguration configuration = new(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine,
        };

        using StreamWriter writer = file.OpenNewFileStreamWriter();
        using CsvWriter csv = new(writer, configuration);
        await csv.WriteRecordsAsync(items);
    }
}

internal static class OutputGeneratorServiceExtensions
{
    public static FileStream OpenNewFileStream(this FileInfo file) => file.Open(FileMode.Create, FileAccess.Write, FileShare.None);

    public static StreamWriter OpenNewFileStreamWriter(this FileInfo file) => new(file.OpenNewFileStream());
}

internal static partial class Logging
{
    public static readonly Action<ILogger, string, string, Exception?> LogGeneratingOutputAction =
        LoggerMessage.Define<string, string>(LogLevel.Information, new EventId(1, nameof(OutputGeneratorService)), "Generating output file:\t{FileName} [{FileName}]");

    public static void LogGeneratingOutput(this ILogger logger, Format format, FileInfo file)
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            LogGeneratingOutputAction(logger, $"{file.FullName}", $"{format}", default);
        }
    }
}