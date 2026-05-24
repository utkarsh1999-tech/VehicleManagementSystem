using VehicleManagementSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VehicleManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class CommissionController : ControllerBase
{
    private readonly ICommissionService _commissionService;

    public CommissionController(
        ICommissionService commissionService)
    {
        _commissionService = commissionService;
    }

    [HttpGet("report")]
    public async Task<IActionResult> GetReport()
        => Ok(await _commissionService.GenerateReportAsync());
}