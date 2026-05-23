using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.Core.DTOs;

public class CreateCarModelDto
{
    [Required]
    [RegularExpression("^(Audi|Jaguar|Land Rover|Renault)$", ErrorMessage = "Invalid brand.")]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^(A-Class|B-Class|C-Class)$", ErrorMessage = "Invalid class.")]
    public string Class { get; set; } = string.Empty;

    [Required]
    public string ModelName { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^[a-zA-Z0-9]{10}$", ErrorMessage = "Model Code must be exactly 10 alphanumeric characters.")]
    public string ModelCode { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Features { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

    [Required]
    public DateTime DateOfManufacturing { get; set; }

    public bool IsActive { get; set; } = true;

    [Range(0, int.MaxValue)]
    public int SortOrder { get; set; }
}
