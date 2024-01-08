namespace WebMVC.Models;

public class ListBooksViewModel
{
    public string? FilteredGenreName { get; set; }
    public string? FilteredPublisherName { get; set; }
    public ICollection<BookViewModel> Books { get; set; }
}