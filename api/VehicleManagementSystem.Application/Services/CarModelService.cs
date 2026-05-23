using VehicleManagementSystem.Core.DTOs;
using VehicleManagementSystem.Core.Interfaces;
using VehicleManagementSystem.Core.Models;

namespace VehicleManagementSystem.Application.Services;

public class CarModelService : ICarModelService
{
    private readonly ICarModelRepository _repository;

    public CarModelService(ICarModelRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CarModelDto>> GetAllAsync()
    {
        var models = await _repository.GetAllAsync();
        return models.Select(MapToDto);
    }

    public async Task<CarModelDto?> GetByIdAsync(int id)
    {
        var model = await _repository.GetByIdAsync(id);
        return model is null ? null : MapToDto(model);
    }

    public async Task<int> CreateAsync(CreateCarModelDto dto)
    {
        var model = MapFromDto(dto);
        return await _repository.AddAsync(model);
    }

    public async Task<bool> UpdateAsync(int id, CreateCarModelDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return false;

        var updated = MapFromDto(dto);
        updated.Id = id;
        updated.UpdatedAt = DateTime.UtcNow;
        return await _repository.UpdateAsync(updated);
    }

    public async Task<bool> DeleteAsync(int id)
        => await _repository.DeleteAsync(id);

    public async Task<IEnumerable<CarModelDto>> SearchAsync(string modelName, string modelCode)
    {
        var results = await _repository.SearchAsync(modelName, modelCode);
        return results.Select(MapToDto);
    }

    public async Task AddImageAsync(int carModelId, string imagePath)
        => await _repository.AddImageAsync(carModelId, imagePath);

    private static CarModelDto MapToDto(CarModel m) => new()
    {
        Id = m.Id, Brand = m.Brand, Class = m.Class,
        ModelName = m.ModelName, ModelCode = m.ModelCode,
        Description = m.Description, Features = m.Features,
        Price = m.Price, DateOfManufacturing = m.DateOfManufacturing,
        IsActive = m.IsActive, SortOrder = m.SortOrder,
        ImagePaths = m.ImagePaths
    };

    private static CarModel MapFromDto(CreateCarModelDto dto) => new()
    {
        Brand = dto.Brand, Class = dto.Class,
        ModelName = dto.ModelName, ModelCode = dto.ModelCode,
        Description = dto.Description, Features = dto.Features,
        Price = dto.Price, DateOfManufacturing = dto.DateOfManufacturing,
        IsActive = dto.IsActive, SortOrder = dto.SortOrder
    };
}
