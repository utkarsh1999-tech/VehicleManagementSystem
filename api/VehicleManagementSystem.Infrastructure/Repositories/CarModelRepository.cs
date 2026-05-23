using VehicleManagementSystem.Core.Interfaces;
using VehicleManagementSystem.Core.Models;

namespace VehicleManagementSystem.Infrastructure.Repositories;

public class CarModelRepository : ICarModelRepository
{
    private static readonly List<CarModel> _carModels = new()
    {
        new() { 
            Id = 1, 
            Brand = "Audi", 
            Class = "A-Class", 
            ModelName = "e-tron GT", 
            ModelCode = "AUDIETR10A", 
            Description = "<p>High performance electric sedan with stunning styling and agility.</p>", 
            Features = "<p>Quattro AWD, 93 kWh battery, 522 hp boost mode.</p>", 
            Price = 104000m, 
            DateOfManufacturing = new DateTime(2024, 5, 10), 
            IsActive = true, 
            SortOrder = 1, 
            ImagePaths = new() 
        },
        new() { 
            Id = 2, 
            Brand = "Jaguar", 
            Class = "A-Class", 
            ModelName = "F-Type R-Dynamic", 
            ModelCode = "JAGFTYP12B", 
            Description = "<p>Supercharged performance coupe combining luxury with raw power.</p>", 
            Features = "<p>5.0L Supercharged V8, active exhaust system, AWD.</p>", 
            Price = 77300m, 
            DateOfManufacturing = new DateTime(2023, 11, 15), 
            IsActive = true, 
            SortOrder = 2, 
            ImagePaths = new() 
        },
        new() { 
            Id = 3, 
            Brand = "Land Rover", 
            Class = "B-Class", 
            ModelName = "Defender 110", 
            ModelCode = "LNDDEF110C", 
            Description = "<p>Iconic off-road vehicle built for exploration and ultimate durability.</p>", 
            Features = "<p>P400 MHEV engine, electronic air suspension, terrain response.</p>", 
            Price = 60800m, 
            DateOfManufacturing = new DateTime(2024, 2, 20), 
            IsActive = true, 
            SortOrder = 3, 
            ImagePaths = new() 
        },
        new() { 
            Id = 4, 
            Brand = "Renault", 
            Class = "C-Class", 
            ModelName = "Clio E-Tech", 
            ModelCode = "RENCLIO23D", 
            Description = "<p>Efficient city hatchback with smart hybrid technology and style.</p>", 
            Features = "<p>E-Tech full hybrid 145, 18-inch alloy wheels, multi-sense modes.</p>", 
            Price = 22000m, 
            DateOfManufacturing = new DateTime(2023, 8, 5), 
            IsActive = true, 
            SortOrder = 4, 
            ImagePaths = new() 
        },
        new() { 
            Id = 5, 
            Brand = "Audi", 
            Class = "B-Class", 
            ModelName = "Q3 Sportback", 
            ModelCode = "AUDIQ3SB05", 
            Description = "<p>Compact crossover SUV with athletic coupe lines and advanced cockpit.</p>", 
            Features = "<p>TFSI engine, Virtual Cockpit, Lane Departure Warning.</p>", 
            Price = 38200m, 
            DateOfManufacturing = new DateTime(2024, 1, 12), 
            IsActive = true, 
            SortOrder = 5, 
            ImagePaths = new() 
        }
    };

    public Task<IEnumerable<CarModel>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<CarModel>>(
            _carModels.Where(m => m.IsActive)
                      .OrderBy(m => m.SortOrder)
                      .ThenByDescending(m => m.DateOfManufacturing)
        );
    }

    public Task<CarModel?> GetByIdAsync(int id)
    {
        return Task.FromResult(_carModels.FirstOrDefault(m => m.Id == id));
    }

    public Task<int> AddAsync(CarModel entity)
    {
        var nextId = _carModels.Count > 0 ? _carModels.Max(m => m.Id) + 1 : 1;
        entity.Id = nextId;
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        _carModels.Add(entity);
        return Task.FromResult(nextId);
    }

    public Task<bool> UpdateAsync(CarModel entity)
    {
        var idx = _carModels.FindIndex(m => m.Id == entity.Id);
        if (idx == -1) return Task.FromResult(false);

        entity.UpdatedAt = DateTime.UtcNow;
        
        // Merge image paths if form doesn't provide them
        if (entity.ImagePaths == null || entity.ImagePaths.Count == 0)
        {
            entity.ImagePaths = _carModels[idx].ImagePaths;
        }

        _carModels[idx] = entity;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var idx = _carModels.FindIndex(m => m.Id == id);
        if (idx == -1) return Task.FromResult(false);

        _carModels.RemoveAt(idx);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<CarModel>> SearchAsync(string modelName, string modelCode)
    {
        var query = _carModels.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(modelName))
            query = query.Where(m => m.ModelName.Contains(modelName, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrWhiteSpace(modelCode))
            query = query.Where(m => m.ModelCode.Contains(modelCode, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult<IEnumerable<CarModel>>(
            query.OrderBy(m => m.SortOrder)
                 .ThenByDescending(m => m.DateOfManufacturing)
        );
    }

    public Task<IEnumerable<CarModel>> GetByClassAsync(string carClass)
    {
        return Task.FromResult<IEnumerable<CarModel>>(_carModels.Where(m => m.Class == carClass && m.IsActive));
    }

    public Task AddImageAsync(int carModelId, string imagePath)
    {
        var model = _carModels.FirstOrDefault(m => m.Id == carModelId);
        if (model != null)
        {
            if (model.ImagePaths == null) model.ImagePaths = new();
            model.ImagePaths.Add(imagePath);
        }
        return Task.CompletedTask;
    }

    public Task DeleteImagesAsync(int carModelId)
    {
        var model = _carModels.FirstOrDefault(m => m.Id == carModelId);
        if (model != null)
        {
            model.ImagePaths.Clear();
        }
        return Task.CompletedTask;
    }
}
