namespace Examples.SampleData.Services;

/// <inheritdoc />
public sealed class FileParserService(
    ILogger<DataConverterService> logger
) : IFileParserService
{
    /// <inheritdoc />
    public async IAsyncEnumerable<T> ParseFilesAsync<T>(DirectoryInfo directory)
    {
        ArgumentNullException.ThrowIfNull(directory);

        GlobOptions options = new GlobOptionsBuilder()
            .WithBasePath(directory.FullName)
            .WithPattern("**/*.yaml")
            .Build();

        IAsyncEnumerable<FileInfo> files = options.GetMatchingFileInfosAsync();

        IDeserializer deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        await foreach(FileInfo file in files)
        {
            logger.LogFoundFile(file);

            using FileStream stream = file.OpenRead();
            using StreamReader reader = new(stream);

            T result = deserializer.Deserialize<T>(reader);
            yield return result;
        }
    }
}

internal static partial class Logging
{
    public static readonly Action<ILogger, string, Exception?> LogFoundFileAction =
        LoggerMessage.Define<string>(LogLevel.Information, new EventId(1, nameof(DataConverterService)), "Found file:\t{FileName}");

    public static void LogFoundFile(this ILogger logger, FileInfo file)
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            LogFoundFileAction(logger, file.FullName, default);
        }
    }
}