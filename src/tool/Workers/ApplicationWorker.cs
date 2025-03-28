namespace Examples.SampleData.Tool.Workers;

/// <summary>
/// Represents a background worker that will perform the tasks of this tool.
/// </summary>
internal sealed class ApplicationWorker(
    ILogger<ApplicationWorker> logger,
    Configuration configuration,
    IDataConverterService dataConverterService,
    IHostApplicationLifetime hostApplicationLifetime
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogRunningTime(DateTimeOffset.UtcNow);

        await dataConverterService.ConvertDataAsync(configuration);

        hostApplicationLifetime.StopApplication();
    }
}

internal static partial class Logging
{
    public static readonly Action<ILogger, string, Exception?> LogRunningTimeAction =
        LoggerMessage.Define<string>(LogLevel.Information, new EventId(1, nameof(ApplicationWorker)), "Worker running at:\t{Time}");

    public static void LogRunningTime(this ILogger logger, DateTimeOffset time)
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            LogRunningTimeAction(logger, $"{time:O}", default);
        }
    }
}
