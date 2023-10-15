using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookHubBdContext _dbContext;

    public BooksController(BookHubBdContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ICollection<BookModel>> GetAllProducts()
    {
        var books = await _dbContext.Books
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Select(book => new BookModel
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Name = book.Name,
                Description = book.Description,
                Price = book.Price,
                Authors = book.Authors.Select(a => a.Name).ToList(),
                Publisher = book.Publisher.Name,
            })
            .ToListAsync();

        return books;
    }
}