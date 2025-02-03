namespace Core.Entities;

public class User
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? SalesmanCode { get; set; }

    public string? Company { get; set; }

    public int? DirectManagerId { get; set; }

    public ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public User? DirectManager { get; set; }

    public ICollection<User> InverseDirectManager { get; set; } = new List<User>();

    public ICollection<Measures> Measures { get; set; } = new List<Measures>();
}
