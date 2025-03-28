namespace Examples.SampleData.Models;

/// <summary>
/// Represents an item in the sample data.
/// </summary>
public sealed record Item
{
    /// <summary>
    /// Gets the name of the item.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public required string Vin { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Make { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Model { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Year { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Mileage { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string FuelType { get; init; }
}