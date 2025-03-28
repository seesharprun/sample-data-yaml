namespace Examples.SampleData.Services;

/// <inheritdoc />
public sealed class DataConverterService(
    ILogger<DataConverterService> logger,
    IFileParserService fileParserService,
    IOutputGeneratorService outputGeneratorService
) : IDataConverterService
{
    /// <inheritdoc />
    public async Task ConvertDataAsync(Configuration configuration)
    {
        logger.LogStart();

        ArgumentNullException.ThrowIfNull(configuration);

        await outputGeneratorService.GenerateOutputFileAsync(
            format: configuration.OutputFormat, 
            file: configuration.OutputFile, 
            data: fileParserService.ParseFilesAsync<Item>(configuration.InputDirectory)
        );
    }
}

internal static partial class Logging
{
    public static readonly Action<ILogger, Exception?> LogStartAction =
        LoggerMessage.Define(LogLevel.Information, new EventId(1, nameof(DataConverterService)), "Starting conversion process...");

    public static void LogStart(this ILogger logger)
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            LogStartAction(logger, default);
        }
    }
}