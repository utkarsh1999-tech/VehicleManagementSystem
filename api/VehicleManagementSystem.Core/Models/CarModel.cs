namespace VehicleManagementSystem.Core.Models;

public class CarModel
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public string ModelCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Features { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime DateOfManufacturing { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
    public List<string> ImagePaths { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
