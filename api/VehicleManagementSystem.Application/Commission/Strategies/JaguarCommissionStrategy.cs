using VehicleManagementSystem.Core.Interfaces;

namespace VehicleManagementSystem.Application.Commission.Strategies;

public class JaguarCommissionStrategy : IBrandCommissionStrategy
{
    public string Brand => "Jaguar";

    public decimal FixedCommission(decimal modelPrice)
        => modelPrice > 35_000m ? 750m : 0m;

    public decimal ClassCommissionRate(string carClass)
        => carClass switch
        {
            "A-Class" => 0.06m,
            "B-Class" => 0.05m,
            "C-Class" => 0.03m,
            _ => 0m
        };
}
