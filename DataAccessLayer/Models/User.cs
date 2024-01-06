namespace DataAccessLayer.Models;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsAdministrator { get; set; } = false;
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}