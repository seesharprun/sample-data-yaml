namespace Examples.SampleData.Models;

/// <summary>
/// Represents options for the tool.
/// </summary>
public sealed record Configuration
{
    /// <summary>
    /// Gets the output file.
    /// </summary>
    public required FileInfo OutputFile { get; init; }

    /// <summary>
    /// Gets the output format.
    /// </summary>
    public required Format OutputFormat { get; init; }

    /// <summary>
    /// Gets the input directory.
    /// </summary>
    public required DirectoryInfo InputDirectory { get; init; }
}