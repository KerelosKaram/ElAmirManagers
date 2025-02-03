using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ManagerMeasuresTargetController : BaseApiController
{
    private readonly IEmployeeService _employeeService;

    public ManagerMeasuresTargetController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("ManagerMeasuresTarget/{managerUserId}")]
    public async Task<IActionResult> GetManagerMeasuresTarget(int managerUserId)
    {
        var result = await _employeeService.GetEmployeeHierarchyWithMeasureTargetAsync(managerUserId);
        return Ok(result);
    }
}