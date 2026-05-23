using VehicleManagementSystem.Core.DTOs;

namespace VehicleManagementSystem.Core.Interfaces;

public interface ICommissionService
{
    Task<IEnumerable<CommissionReportDto>> GenerateReportAsync();
}
