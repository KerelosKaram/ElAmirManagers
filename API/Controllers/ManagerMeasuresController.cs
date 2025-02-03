using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ManagerMeasuresController : BaseApiController
{
    private readonly IEmployeeService _employeeService;

    public ManagerMeasuresController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("ManagerMeasures/{managerUserId}")]
    public async Task<IActionResult> GetManagerMeasuresTarget(int managerUserId)
    {
        var result = await _employeeService.GetEmployeeHierarchyWithMeasureTargetAsync(managerUserId);
        return Ok(result);
    }
}