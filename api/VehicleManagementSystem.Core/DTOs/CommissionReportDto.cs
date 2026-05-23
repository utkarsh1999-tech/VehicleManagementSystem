namespace VehicleManagementSystem.Core.DTOs;

public class CommissionReportDto
{
    public string SalesmanName { get; set; } = string.Empty;
    public decimal TotalSalesAmount { get; set; }
    public decimal FixedCommission { get; set; }
    public decimal ClassCommission { get; set; }
    public decimal BonusCommission { get; set; }
    public decimal TotalCommission { get; set; }
    public List<CommissionBreakdownDto> Breakdown { get; set; } = new();
}

public class CommissionBreakdownDto
{
    public string Brand { get; set; } = string.Empty;
    public string CarClass { get; set; } = string.Empty;
    public int CarsSold { get; set; }
    public decimal ModelPrice { get; set; }
    public decimal BrandFixedCommission { get; set; }
    public decimal ClassCommissionRate { get; set; }
    public decimal LineTotal { get; set; }
}
