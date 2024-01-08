using System.Diagnostics;
using BusinessLayer.Models.Book;
using BusinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using BookMapper = WebMVC.Mappers.BookMapper;

namespace WebMVC.Controllers;

public class BooksController : Controller
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    [Route("")]
    [Route("books")]
    public async Task<ActionResult> List()
    {
        var books = await _booksService.GetBooksAsync(new GetBooksModel());
        var model = books.Select(BookMapper.MapToBookViewModel);

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