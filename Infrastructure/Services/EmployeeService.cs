using Core.DTO;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UserHierarchyDto> GetEmployeeHierarchyAsync(int managerUserId)
    {
        var rootUser = await _context.Users
            .Where(u => u.UserId == managerUserId)
            .FirstOrDefaultAsync();

        var result = new UserHierarchyDto
        {
            UserId = rootUser!.UserId,
            UserName = rootUser.UserName,
            Title = rootUser.Title,
            SalesmanCode = rootUser.SalesmanCode,
        };

        await GetSubordinatesAsync(managerUserId, result.Subordinates);
        return result;
    }

    private async Task GetSubordinatesAsync(int managerUserId, List<UserHierarchyDto> result)
    {
        var subordinates = await _context.Users
            .Where(u => u.DirectManagerId == managerUserId)
            .ToListAsync();
        
        foreach (var subordinate in subordinates)
        {
            var dto = new UserHierarchyDto
            {
                UserId = subordinate.UserId,
                UserName = subordinate.UserName,
                Title = subordinate.Title,
                SalesmanCode = subordinate.SalesmanCode,
            };
            result.Add(dto);
            await GetSubordinatesAsync(subordinate.UserId, dto.Subordinates);
        }
    }
    
    public async Task<UserHierarchyDto> GetEmployeeHierarchyWithMeasureTargetAsync(int managerUserId)
    {
        var rootUser = await _context.Users
            .Include(u => u.Customers)  // Include customers associated with the user
            .ThenInclude(c => c.Measures)  // Include measures associated with each customer
            .Where(u => u.UserId == managerUserId)
            .FirstOrDefaultAsync();

        if (rootUser == null) return null!;
        
        var result = new UserHierarchyDto
        {
            UserId = rootUser.UserId,
            UserName = rootUser.UserName,
            Title = rootUser.Title,
            MeasureSummaries = CalculateMeasureSummaries(rootUser.Customers)
        };

        
        await GetSubordinatesWithMeasureTargetAsync(managerUserId, result.Subordinates);
        return result;
    }
    private async Task GetSubordinatesWithMeasureTargetAsync(int managerUserId, List<UserHierarchyDto> result)
    {
        var subordinates = await _context.Users
            .Include(u => u.Customers)
            .ThenInclude(c => c.Measures)
            .Where(u => u.DirectManagerId == managerUserId)
            .ToListAsync();



        foreach (var subordinate in subordinates)
        {
            var dto = new UserHierarchyDto
            {
                UserId = subordinate.UserId,
                UserName = subordinate.UserName,
                Title = subordinate.Title,
                SalesmanCode = subordinate.SalesmanCode,
                MeasureSummaries = CalculateMeasureSummaries(subordinate.Customers)
            };
            result.Add(dto);
            await GetSubordinatesWithMeasureTargetAsync(subordinate.UserId, dto.Subordinates);
        }
    }
    private List<MeasureSummaryDto> CalculateMeasureSummaries(IEnumerable<Customer> customers)
    {
        return customers
            .SelectMany(c => c.Measures)
            .GroupBy(m => m.Measure)
            .Select(g => new MeasureSummaryDto
            {
                MeasureName = g.Key!,
                TotalTarget = g.Sum(m => m.Target ?? 0),
                TotalAchieved = g.Sum(m => m.Achieved ?? 0),
                TotalRemaining = g.Sum(m => m.Remaining ?? 0),
                TotalExpected = g.Sum(m => m.Expected ?? 0)
            })
            .ToList();
    }

}