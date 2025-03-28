namespace Examples.SampleData.Services.Interfaces;

/// <summary>
/// Defines the interface for the service that parses files.
/// </summary>
public interface IFileParserService
{
    /// <summary>
    /// Parses a file asynchronously and returns an enumerable of the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the objects to parse from the file.
    /// </typeparam>
    /// <param name="directory">
    /// The path to the files to parse.
    /// </param>
    /// <returns>
    /// An asynchronous enumerable of objects of type <typeparamref name="T"/> parsed from the file.
    /// </returns>
    IAsyncEnumerable<T> ParseFilesAsync<T>(DirectoryInfo directory);
}