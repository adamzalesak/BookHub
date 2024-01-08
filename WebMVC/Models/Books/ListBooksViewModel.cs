using BusinessLayer.Models.Genre;
using BusinessLayer.Models.Publisher;

namespace WebMVC.Models.Books;

public class ListBooksViewModel
{
    public required ICollection<BookViewModel> Books { get; set; }
    public string? FilteredGenreName { get; set; }
    public string? FilteredPublisherName { get; set; }
    public ICollection<GenreModel>? FoundGenres { get; set; }
    public ICollection<PublisherModel>? FoundPublishers { get; set; }
}