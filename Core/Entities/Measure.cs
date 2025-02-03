namespace Core.Entities;

public class Measures
{
    public int MeasureId { get; set; }

    public string? Measure { get; set; }

    public decimal? Target { get; set; }

    public decimal? Achieved { get; set; }

    public decimal? Remaining { get; set; }

    public decimal? Expected { get; set; }

    public int? SalesmanId { get; set; }

    public int? CustomerId { get; set; }

    public string? Company { get; set; }

    public Customer? Customer { get; set; }

    public User? Salesman { get; set; }
}
