using VehicleManagementSystem.Core.Interfaces;

namespace VehicleManagementSystem.Application.Commission.Strategies;

public class RenaultCommissionStrategy : IBrandCommissionStrategy
{
    public string Brand => "Renault";

    public decimal FixedCommission(decimal modelPrice)
        => modelPrice > 20_000m ? 400m : 0m;

    public decimal ClassCommissionRate(string carClass)
        => carClass switch
        {
            "A-Class" => 0.05m,
            "B-Class" => 0.03m,
            "C-Class" => 0.02m,
            _ => 0m
        };
}
