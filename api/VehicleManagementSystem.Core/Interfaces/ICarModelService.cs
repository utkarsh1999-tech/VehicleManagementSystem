using VehicleManagementSystem.Core.DTOs;

namespace VehicleManagementSystem.Core.Interfaces;

public interface ICarModelService
{
    Task<IEnumerable<CarModelDto>> GetAllAsync();
    Task<CarModelDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateCarModelDto dto);
    Task<bool> UpdateAsync(int id, CreateCarModelDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<CarModelDto>> SearchAsync(string modelName, string modelCode);
    Task AddImageAsync(int carModelId, string imagePath);
}
