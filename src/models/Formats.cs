namespace Examples.SampleData.Models;

/// <summary>
/// Represents data formats.
/// </summary>
public enum Format
{
    /// <summary>
    /// Represents an unspecified format.
    /// </summary>
    None = 0,

    /// <summary>
    /// Represents the YAML format.
    /// </summary>
    /// <remarks>
    /// This is the default input format.
    /// </remarks>
    Yaml = 1,

    /// <summary>
    /// Represents the JSON format.
    /// </summary>
    Json = 2,

    /// <summary>
    /// Represents the XML format.
    /// </summary>
    Xml = 3,

    /// <summary>
    /// Represents the CSV format.
    /// </summary>
    Csv = 4
}