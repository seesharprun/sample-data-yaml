namespace Examples.SampleData.Services.Interfaces;

/// <summary>
/// Service for converting data between different formats.
/// </summary>
public interface IDataConverterService
{
    /// <summary>
    /// Performs an asynchronous conversion of data between formats using the specificed options.
    /// </summary>
    /// <param name="configuration">
    /// The configuration for the conversion.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task ConvertDataAsync(Configuration configuration);
}