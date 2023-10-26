namespace DataAccessLayer.Models;

public class Book : BaseEntity
{
    public string Isbn { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ICollection<Author> Authors { get; set; } = new List<Author>();
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
}