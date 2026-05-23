namespace VehicleManagementSystem.Core.Interfaces;

public interface IBrandCommissionStrategy
{
    string Brand { get; }
    decimal FixedCommission(decimal modelPrice);
    decimal ClassCommissionRate(string carClass);
}
