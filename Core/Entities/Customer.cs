namespace Core.Entities;

public class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerCode { get; set; }

    public string? CustomerName { get; set; }

    public string? Address { get; set; }

    public decimal? Lat { get; set; }

    public decimal? Long { get; set; }

    public string? Phone { get; set; }

    public string? Classification { get; set; }

    public int? SalesmanId { get; set; }

    public string? Company { get; set; }

    public ICollection<Measures> Measures { get; set; } = new List<Measures>();

    public User? Salesman { get; set; }
}
