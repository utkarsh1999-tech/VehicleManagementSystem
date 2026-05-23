using VehicleManagementSystem.Application.Commission;
using VehicleManagementSystem.Core.DTOs;
using VehicleManagementSystem.Core.Interfaces;
using VehicleManagementSystem.Core.Models;

namespace VehicleManagementSystem.Application.Services;

public class CommissionService : ICommissionService
{
    private readonly CommissionStrategyFactory _factory;
    private readonly ICarModelRepository _carModelRepository;

    private static readonly List<Salesman> Salesmen = new()
    {
        new() { Id = 1, Name = "Salesman 1 (John Smith)",      PreviousYearSalesAmount = 490_000m },
        new() { Id = 2, Name = "Salesman 2 (Richard Porter)",  PreviousYearSalesAmount = 1_000_000m },
        new() { Id = 3, Name = "Salesman 3 (Tony Grid)",       PreviousYearSalesAmount = 650_000m },
    };

    private static readonly List<SalesRecord> SalesData = new()
    {
        new() { SalesmanId=1, CarClass="A-Class", Brand="Audi",       NumberOfCarsSold=1 },
        new() { SalesmanId=1, CarClass="A-Class", Brand="Jaguar",     NumberOfCarsSold=3 },
        new() { SalesmanId=1, CarClass="A-Class", Brand="Land Rover", NumberOfCarsSold=0 },
        new() { SalesmanId=1, CarClass="A-Class", Brand="Renault",    NumberOfCarsSold=6 },
        new() { SalesmanId=1, CarClass="B-Class", Brand="Audi",       NumberOfCarsSold=2 },
        new() { SalesmanId=1, CarClass="B-Class", Brand="Jaguar",     NumberOfCarsSold=4 },
        new() { SalesmanId=1, CarClass="B-Class", Brand="Land Rover", NumberOfCarsSold=2 },
        new() { SalesmanId=1, CarClass="B-Class", Brand="Renault",    NumberOfCarsSold=2 },
        new() { SalesmanId=1, CarClass="C-Class", Brand="Audi",       NumberOfCarsSold=3 },
        new() { SalesmanId=1, CarClass="C-Class", Brand="Jaguar",     NumberOfCarsSold=6 },
        new() { SalesmanId=1, CarClass="C-Class", Brand="Land Rover", NumberOfCarsSold=1 },
        new() { SalesmanId=1, CarClass="C-Class", Brand="Renault",    NumberOfCarsSold=1 },

        new() { SalesmanId=2, CarClass="A-Class", Brand="Audi",       NumberOfCarsSold=0 },
        new() { SalesmanId=2, CarClass="A-Class", Brand="Jaguar",     NumberOfCarsSold=5 },
        new() { SalesmanId=2, CarClass="A-Class", Brand="Land Rover", NumberOfCarsSold=5 },
        new() { SalesmanId=2, CarClass="A-Class", Brand="Renault",    NumberOfCarsSold=3 },
        new() { SalesmanId=2, CarClass="B-Class", Brand="Audi",       NumberOfCarsSold=0 },
        new() { SalesmanId=2, CarClass="B-Class", Brand="Jaguar",     NumberOfCarsSold=4 },
        new() { SalesmanId=2, CarClass="B-Class", Brand="Land Rover", NumberOfCarsSold=2 },
        new() { SalesmanId=2, CarClass="B-Class", Brand="Renault",    NumberOfCarsSold=2 },
        new() { SalesmanId=2, CarClass="C-Class", Brand="Audi",       NumberOfCarsSold=0 },
        new() { SalesmanId=2, CarClass="C-Class", Brand="Jaguar",     NumberOfCarsSold=2 },
        new() { SalesmanId=2, CarClass="C-Class", Brand="Land Rover", NumberOfCarsSold=1 },
        new() { SalesmanId=2, CarClass="C-Class", Brand="Renault",    NumberOfCarsSold=1 },

        new() { SalesmanId=3, CarClass="A-Class", Brand="Audi",       NumberOfCarsSold=4 },
        new() { SalesmanId=3, CarClass="A-Class", Brand="Jaguar",     NumberOfCarsSold=2 },
        new() { SalesmanId=3, CarClass="A-Class", Brand="Land Rover", NumberOfCarsSold=1 },
        new() { SalesmanId=3, CarClass="A-Class", Brand="Renault",    NumberOfCarsSold=6 },
        new() { SalesmanId=3, CarClass="B-Class", Brand="Audi",       NumberOfCarsSold=2 },
        new() { SalesmanId=3, CarClass="B-Class", Brand="Jaguar",     NumberOfCarsSold=7 },
        new() { SalesmanId=3, CarClass="B-Class", Brand="Land Rover", NumberOfCarsSold=2 },
        new() { SalesmanId=3, CarClass="B-Class", Brand="Renault",    NumberOfCarsSold=3 },
        new() { SalesmanId=3, CarClass="C-Class", Brand="Audi",       NumberOfCarsSold=0 },
        new() { SalesmanId=3, CarClass="C-Class", Brand="Jaguar",     NumberOfCarsSold=1 },
        new() { SalesmanId=3, CarClass="C-Class", Brand="Land Rover", NumberOfCarsSold=3 },
        new() { SalesmanId=3, CarClass="C-Class", Brand="Renault",    NumberOfCarsSold=1 },
    };

    public CommissionService(CommissionStrategyFactory factory, ICarModelRepository carModelRepository)
    {
        _factory = factory;
        _carModelRepository = carModelRepository;
    }

    public async Task<IEnumerable<CommissionReportDto>> GenerateReportAsync()
    {
        var allModels = (await _carModelRepository.GetAllAsync()).ToList();
        var reports = new List<CommissionReportDto>();

        foreach (var salesman in Salesmen)
        {
            var salesmanRecords = SalesData.Where(s => s.SalesmanId == salesman.Id).ToList();
            var breakdown = new List<CommissionBreakdownDto>();

            decimal totalFixed = 0, totalClass = 0, totalSales = 0;

            foreach (var record in salesmanRecords.Where(r => r.NumberOfCarsSold > 0))
            {
                var strategy = _factory.Resolve(record.Brand);

                var avgPrice = allModels
                    .Where(m => m.Brand == record.Brand && m.Class == record.CarClass && m.IsActive)
                    .Select(m => m.Price)
                    .DefaultIfEmpty(0m)
                    .Average();

                decimal lineFixed  = strategy.FixedCommission(avgPrice) * record.NumberOfCarsSold;
                decimal classRate  = strategy.ClassCommissionRate(record.CarClass);
                decimal lineClass  = avgPrice * classRate * record.NumberOfCarsSold;
                decimal lineSales  = avgPrice * record.NumberOfCarsSold;

                totalFixed  += lineFixed;
                totalClass  += lineClass;
                totalSales  += lineSales;

                breakdown.Add(new CommissionBreakdownDto
                {
                    Brand = record.Brand,
                    CarClass = record.CarClass,
                    CarsSold = record.NumberOfCarsSold,
                    ModelPrice = avgPrice,
                    BrandFixedCommission = lineFixed,
                    ClassCommissionRate = classRate,
                    LineTotal = lineFixed + lineClass
                });
            }

            decimal bonus = 0m;
            if (salesman.PreviousYearSalesAmount > 500_000m)
            {
                decimal classASales = salesmanRecords
                    .Where(r => r.CarClass == "A-Class")
                    .Sum(r =>
                    {
                        var avgP = allModels
                            .Where(m => m.Brand == r.Brand && m.Class == "A-Class" && m.IsActive)
                            .Select(m => m.Price).DefaultIfEmpty(0m).Average();
                        return avgP * r.NumberOfCarsSold;
                    });
                bonus = classASales * 0.02m;
            }

            reports.Add(new CommissionReportDto
            {
                SalesmanName     = salesman.Name,
                TotalSalesAmount = totalSales,
                FixedCommission  = totalFixed,
                ClassCommission  = totalClass,
                BonusCommission  = bonus,
                TotalCommission  = totalFixed + totalClass + bonus,
                Breakdown        = breakdown
            });
        }

        return reports;
    }
}
