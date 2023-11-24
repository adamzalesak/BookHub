namespace DataAccessLayer.Models;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsAdministrator { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Order> Orders { get; set; }
}