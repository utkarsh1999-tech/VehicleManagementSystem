namespace VehicleManagementSystem.Core.Models;

public class SalesRecord
{
    public int Id { get; set; }
    public int SalesmanId { get; set; }
    public string CarClass { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public int NumberOfCarsSold { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}
