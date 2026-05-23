using VehicleManagementSystem.Core.Interfaces;

namespace VehicleManagementSystem.Application.Commission;

public class CommissionStrategyFactory
{
    private readonly IReadOnlyDictionary<string, IBrandCommissionStrategy> _strategies;

    public CommissionStrategyFactory(IEnumerable<IBrandCommissionStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(s => s.Brand, StringComparer.OrdinalIgnoreCase);
    }

    public IBrandCommissionStrategy Resolve(string brand)
    {
        if (_strategies.TryGetValue(brand, out var strategy))
            return strategy;

        throw new InvalidOperationException($"No commission strategy registered for brand '{brand}'.");
    }
}
