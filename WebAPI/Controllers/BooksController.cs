using BusinessLayer.Exceptions;
using BusinessLayer.Models.Book;
using BusinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    /// <summary>
    /// Get all books
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<BookPaginationModel>> GetBooks(
        [FromQuery] GetBooksModel parameters
    )
    {
        var books = await _booksService.GetBooksAsync(parameters);
        return Ok(books);
    }

    /// <summary>
    /// Get book by id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookModel>> GetBook(int id)
    {
        var book = await _booksService.GetBookAsync(id);
        if (book == null)
        {
            return NotFound("Book not found.");
        }

        return Ok(book);
    }

    /// <summary>
    /// Create book
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> CreateBook(CreateBookModel model)
    {
        try
        {
            var newBookModel = await _booksService.CreateBookAsync(model);
            await _booksService.SaveAsync();
            return Created($"/books/{newBookModel.Id}", newBookModel);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Edit book
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditBook([FromRoute] int id, EditBookModel model)
    {
        try
        {
            await _booksService.EditBookAsync(id, model);
            await _booksService.SaveAsync();
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Delete book
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        try
        {
            await _booksService.DeleteBookAsync(id);
            await _booksService.SaveAsync();
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}