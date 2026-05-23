using VehicleManagementSystem.Core.Interfaces;

namespace VehicleManagementSystem.Application.Commission.Strategies;

public class AudiCommissionStrategy : IBrandCommissionStrategy
{
    public string Brand => "Audi";

    public decimal FixedCommission(decimal modelPrice)
        => modelPrice > 25_000m ? 800m : 0m;

    public decimal ClassCommissionRate(string carClass)
        => carClass switch
        {
            "A-Class" => 0.08m,
            "B-Class" => 0.06m,
            "C-Class" => 0.04m,
            _ => 0m
        };
}
