using System.Diagnostics;
using BusinessLayer.Models.Book;
using BusinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Models.Books;
using BookMapper = WebMVC.Mappers.BookMapper;

namespace WebMVC.Controllers;

public class BooksController : Controller
{
    private readonly IBooksService _booksService;
    private readonly IGenreService _genresService;
    private readonly IPublishersService _publishersService;

    public BooksController(IBooksService booksService,
        IGenreService genresService,
        IPublishersService publishersService)
    {
        _booksService = booksService;
        _genresService = genresService;
        _publishersService = publishersService;
    }


    [Route("")]
    [Route("books")]
    public async Task<ActionResult> List([FromQuery] int? genreId, [FromQuery] int? publisherId, string? searchString)
    {
        var books = await _booksService.GetBooksAsync(new GetBooksModel
        {
            GenreId = genreId,
            PublisherId = publisherId,
            Name = searchString,
        });
        var bookModels = books.Select(BookMapper.MapToBookViewModel).ToList();

        var genre = genreId is null ? null : await _genresService.GetGenreByIdAsync(genreId.Value);
        var publisher = publisherId is null ? null : await _publishersService.GetPublisherByIdAsync(publisherId.Value);
        
        var foundGenres = searchString is null ? null : await _genresService.GetGenresAsync(searchString);
        var foundPublishers = searchString is null ? null : await _publishersService.GetPublishersAsync(searchString);
        
        var model = new ListBooksViewModel
        {
            Books = bookModels,
            FilteredGenreName = genre?.Name,
            FilteredPublisherName = publisher?.Name,
            FoundGenres = foundGenres,
            FoundPublishers = foundPublishers,
        };

        return View(model);
    }

    [Route("books/{bookId:int}")]
    public async Task<ActionResult> Detail(int bookId)
    {
        var book = await _booksService.GetBookAsync(bookId);

        if (book is null)
        {
            return NotFound();
        }

        var model = BookMapper.MapToBookViewModel(book);

        return View(model);
    }
}