using VehicleManagementSystem.Core.DTOs;
using VehicleManagementSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VehicleManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CarModelController : ControllerBase
{
    private readonly ICarModelService _service;

    public CarModelController(ICarModelService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var model = await _service.GetByIdAsync(id);
        return model is null ? NotFound() : Ok(model);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string modelName = "", [FromQuery] string modelCode = "")
        => Ok(await _service.SearchAsync(modelName, modelCode));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCarModelDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateCarModelDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var success = await _service.UpdateAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }

    [HttpPost("{id:int}/images")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UploadImages(int id, [FromForm] List<IFormFile> files)
    {
        if (files == null || files.Count == 0) return BadRequest("No files provided.");

        var uploadDir = Path.Combine("wwwroot", "uploads", "carmodels", id.ToString());
        Directory.CreateDirectory(uploadDir);

        var savedPaths = new List<string>();
        foreach (var file in files)
        {
            if (file.Length > 5 * 1024 * 1024)
                return BadRequest($"File '{file.FileName}' exceeds 5 MB.");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadDir, fileName);
            await using var stream = System.IO.File.Create(filePath);
            await file.CopyToAsync(stream);
            
            var relativePath = $"/uploads/carmodels/{id}/{fileName}";
            savedPaths.Add(relativePath);
            
            await _service.AddImageAsync(id, relativePath);
        }

        return Ok(new { ImagePaths = savedPaths });
    }
}
