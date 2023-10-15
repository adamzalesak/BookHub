namespace WebAPI.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Isbn { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<string> Authors { get; set; }
    public string Publisher { get; set; }
}