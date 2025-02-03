namespace Core.DTO;

public class UserHierarchyDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Title { get; set; }
    public string? SalesmanCode { get; set; }
    public List<UserHierarchyDto> Subordinates { get; set; } = new List<UserHierarchyDto>();
    public List<MeasureSummaryDto>? MeasureSummaries { get; set; } = new List<MeasureSummaryDto>();
}