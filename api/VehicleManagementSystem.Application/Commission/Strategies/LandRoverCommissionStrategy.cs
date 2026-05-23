using VehicleManagementSystem.Core.Interfaces;

namespace VehicleManagementSystem.Application.Commission.Strategies;

public class LandRoverCommissionStrategy : IBrandCommissionStrategy
{
    public string Brand => "Land Rover";

    public decimal FixedCommission(decimal modelPrice)
        => modelPrice > 30_000m ? 850m : 0m;

    public decimal ClassCommissionRate(string carClass)
        => carClass switch
        {
            "A-Class" => 0.07m,
            "B-Class" => 0.05m,
            "C-Class" => 0.04m,
            _ => 0m
        };
}
