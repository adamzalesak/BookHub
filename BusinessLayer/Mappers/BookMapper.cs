using BusinessLayer.Models.Book;
using DataAccessLayer.Models;
using Riok.Mapperly.Abstractions;

namespace BusinessLayer.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class BookMapper
{
    public static partial IQueryable<BookModel> ProjectToModel(this IQueryable<Book> q);
    public static partial Book MapCreateBookModelToBook(this CreateBookModel model);
    public static partial BookModel MapBookToBookModel(this Book book);

    private static string AuthorToAuthor(Author author) => author.Name;
    private static string PublisherToPublisher(Publisher publisher) => publisher.Name;
    private static string GenreToGenre(Genre genre) => genre.Name;
}
