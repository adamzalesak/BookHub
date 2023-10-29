namespace DataAccessLayer.Models;

public class Cart : BaseEntity
{
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public Order? Order { get; set; }
}