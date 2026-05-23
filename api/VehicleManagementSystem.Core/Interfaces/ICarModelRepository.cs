using VehicleManagementSystem.Core.Models;

namespace VehicleManagementSystem.Core.Interfaces;

public interface ICarModelRepository : IRepository<CarModel>
{
    Task<IEnumerable<CarModel>> SearchAsync(string modelName, string modelCode);
    Task<IEnumerable<CarModel>> GetByClassAsync(string carClass);
    Task AddImageAsync(int carModelId, string imagePath);
    Task DeleteImagesAsync(int carModelId);
}
