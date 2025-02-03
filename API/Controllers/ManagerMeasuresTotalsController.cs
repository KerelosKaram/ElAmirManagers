using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ManagerMeasuresTotalsController : BaseApiController
{
    private readonly IEmployeeService _employeeService;

    public ManagerMeasuresTotalsController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("ManagerMeasuresTotals/{managerUserId}")]
    public async Task<IActionResult> GetManagerMeasuresTarget(int managerUserId)
    {
        var result = await _employeeService.GetEmployeeHierarchyWithMeasureTargetTotalsAsync(managerUserId);
        return Ok(result);
    }
}