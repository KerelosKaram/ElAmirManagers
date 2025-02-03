using Core.DTO;
using Core.Entities;

namespace Core.Interfaces;

public interface IEmployeeService
{
    Task<UserHierarchyDto> GetEmployeeHierarchyAsync(int managerUserId);
    Task<UserHierarchyDto> GetEmployeeHierarchyWithMeasureTargetAsync(int managerUserId);
}