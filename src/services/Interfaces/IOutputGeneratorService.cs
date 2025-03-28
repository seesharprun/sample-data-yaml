namespace Examples.SampleData.Services.Interfaces;

/// <summary>
/// Defines the interface for the service that generates output files.
/// </summary>
public interface IOutputGeneratorService
{
    /// <summary>
    /// Generates an output file asynchronously from the specified data.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the objects to write to the file.
    /// </typeparam>
    /// <param name="format">
    /// The format of the output file (e.g., CSV, JSON).
    /// </param>
    /// <param name="file">
    /// The path to the output file.
    /// </param>
    /// <param name="data">
    /// The data to write to the file.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task GenerateOutputFileAsync<T>(Format format, FileInfo file, IAsyncEnumerable<T> data);
}