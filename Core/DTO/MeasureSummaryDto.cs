namespace Core.DTO;

public class MeasureSummaryDto
{
    public string MeasureName { get; set; }
    public decimal TotalTarget { get; set; }
    public decimal TotalAchieved { get; set; }
    public decimal TotalRemaining { get; set; }
    public decimal TotalExpected { get; set; }
}