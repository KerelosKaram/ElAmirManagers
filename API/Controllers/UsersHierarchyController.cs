using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersHierarchyController : BaseApiController
    {
        private readonly IEmployeeService _employeeService;

        public UsersHierarchyController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("hierarchy/{userId}")]
        public async Task<IActionResult> GetEmployeeHierarchy(int userId)
        {
            var hierarchy = await _employeeService.GetEmployeeHierarchyAsync(userId);
            return Ok(hierarchy);
        }
    }
}