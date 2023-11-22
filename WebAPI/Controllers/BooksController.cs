using System.Globalization;
using System.Linq.Expressions;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.Book;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookHubBdContext _dbContext;
    private readonly IBooksService _booksService;

    public BooksController(BookHubBdContext dbContext, IBooksService booksService)
    {
        _dbContext = dbContext;
        _booksService = booksService;
    }

    /// <summary>
    /// Get all books
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ICollection<BookModel>>> GetBooks(
        [FromQuery] GetBookParamsModel parameters
    )
    {
        if (parameters.AuthorId is not null)
        {
            var author = await _dbContext.Authors.FindAsync(parameters.AuthorId);
            if (author == null)
            {
                return NotFound("Author not found.");
            }
        }

        if (parameters.PublisherId is not null)
        {
            var publisher = await _dbContext.Publishers.FindAsync(parameters.PublisherId);
            if (publisher == null)
            {
                return NotFound("Publisher not found.");
            }
        }

        if (parameters.GenreId is not null)
        {
            var genre = await _dbContext.Genres.FindAsync(parameters.GenreId);
            if (genre == null)
            {
                return NotFound("Genre not found.");
            }
        }

        Expression<Func<Book, bool>> namePredicate = string.IsNullOrWhiteSpace(parameters.Name)
            ? book => true
            : book => book.Name.ToLower().Contains(parameters.Name.ToLower());

        Expression<Func<Book, bool>> descriptionPredicate = string.IsNullOrWhiteSpace(parameters.Description)
            ? book => true
            : book => book.Description.ToLower().Contains(parameters.Description.ToLower());

        Expression<Func<Book, bool>> authorIdPredicate = parameters.AuthorId == null
            ? book => true
            : book => book.Authors.Any(a => a.Id == parameters.AuthorId);

        Expression<Func<Book, bool>> authorNamePredicate = string.IsNullOrWhiteSpace(parameters.AuthorName)
            ? book => true
            : book => book.Authors.Any(a => a.Name.ToLower().Contains(parameters.AuthorName.ToLower()));

        Expression<Func<Book, bool>> publisherIdPredicate = parameters.PublisherId == null
            ? book => true
            : book => book.Publisher.Id == parameters.PublisherId;

        Expression<Func<Book, bool>> publisherNamePredicate = string.IsNullOrWhiteSpace(parameters.PublisherName)
            ? book => true
            : book => book.Publisher.Name.ToLower().Contains(parameters.PublisherName.ToLower());

        Expression<Func<Book, bool>> genreIdPredicate = parameters.GenreId == null
            ? book => true
            : book => book.Genres.Any(g => g.Id == parameters.GenreId);

        Expression<Func<Book, bool>> genreNamePredicate = string.IsNullOrWhiteSpace(parameters.GenreName)
            ? book => true
            : book => book.Genres.Any(g => g.Name.ToLower().Contains(parameters.GenreName.ToLower()));

        Expression<Func<Book, bool>> pricePredicate = book =>
            (parameters.MinPrice == null || book.Prices.OrderByDescending(p => p.ValidFrom).First().BookPrice >= parameters.MinPrice) &&
            (parameters.MaxPrice == null || book.Prices.OrderByDescending(p => p.ValidFrom).First().BookPrice <= parameters.MaxPrice);

        var booksQuery = _dbContext.Books
            .Where(namePredicate)
            .Where(descriptionPredicate)
            .Where(authorIdPredicate)
            .Where(authorNamePredicate)
            .Where(publisherIdPredicate)
            .Where(publisherNamePredicate)
            .Where(genreIdPredicate)
            .Where(genreNamePredicate)
            .Where(pricePredicate);

        if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
        {
            Expression<Func<Book, string?>> orderFunction = book =>
                parameters.OrderBy == "name" ? book.Name :
                parameters.OrderBy == "description" ? book.Description :
                parameters.OrderBy == "price" ? book.Prices.OrderByDescending(p => p.ValidFrom).First().BookPrice
                    .ToString(CultureInfo.InvariantCulture) :
                parameters.OrderBy == "publisher" ? book.Publisher.Name : book.Name;

            if (parameters.OrderDesc == true)
            {
                booksQuery = booksQuery
                    .OrderByDescending(orderFunction);
            }
            else
            {
                booksQuery = booksQuery
                    .OrderBy(orderFunction);
            }
        }

        var books = await booksQuery
            .Skip((parameters.Page ?? 0) * (parameters.PageSize ?? 10))
            .Take(parameters.PageSize ?? 10)
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.Prices)
            .Select(book => new BookModel
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Name = book.Name,
                Description = book.Description,
                Count = book.Count,
                Price = book.Prices.OrderByDescending(p => p.ValidFrom).First().BookPrice,
                Authors = book.Authors.Select(a => a.Name).ToList(),
                Genres = book.Genres.Select(g => g.Name).ToList(),
                Publisher = book.Publisher.Name,
            }).ToListAsync();


        return books;
    }

    /// <summary>
    /// Get book by id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookModel>> GetBook(int id)
    {
        var book = await _dbContext.Books
            .Where(b => b.Id == id)
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Include(b => b.Prices)
            .Select(book => new BookModel
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Name = book.Name,
                Description = book.Description,
                Count = book.Count,
                Price = book.Prices.OrderByDescending(p => p.ValidFrom).First().BookPrice,
                Authors = book.Authors.Select(a => a.Name).ToList(),
                Genres = book.Genres.Select(g => g.Name).ToList(),
                Publisher = book.Publisher.Name,
            }).FirstOrDefaultAsync();

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
        var publisher = await _dbContext.Publishers.FindAsync(model.PublisherId);
        if (publisher == null)
        {
            return NotFound("Publisher not found.");
        }

        var authors = await _dbContext.Authors.Where(a => model.AuthorIds.Contains(a.Id)).ToListAsync();
        if (authors.Count != model.AuthorIds.Count)
        {
            return NotFound("Author not found.");
        }

        var genres = await _dbContext.Genres.Where(g => model.GenreIds.Contains(g.Id)).ToListAsync();
        if (genres.Count != model.GenreIds.Count)
        {
            return NotFound("Genre not found.");
        }

        var newBook = new Book
        {
            Isbn = model.Isbn,
            Name = model.Name,
            Description = model.Description,
            Count = model.Count,
            Authors = authors,
            Genres = genres,
            PublisherId = model.PublisherId,
        };

        var newPrice = new Price
        {
            BookPrice = model.Price,
            ValidFrom = DateTime.Now,
        };
        newBook.Prices.Add(newPrice);

        await _dbContext.Books.AddAsync(newBook);
        await _dbContext.SaveChangesAsync();

        var newBookModel = new BookModel
        {
            Id = newBook.Id,
            Isbn = newBook.Isbn,
            Name = newBook.Name,
            Description = newBook.Description,
            Count = newBook.Count,
            Price = newPrice.BookPrice,
            Authors = newBook.Authors.Select(a => a.Name).ToList(),
            Genres = newBook.Genres.Select(g => g.Name).ToList(),
            Publisher = newBook.Publisher.Name,
        };

        return Created($"/books/{newBookModel.Id}", newBookModel);
    }

    /// <summary>
    /// Edit book
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditBook([FromRoute] int id, EditBookModel model)
    {
        var book = await _dbContext.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
        {
            return NotFound("Book not found.");
        }

        if (model.PublisherId is not null)
        {
            var publisher = await _dbContext.Publishers.FindAsync(model.PublisherId);
            if (publisher == null)
            {
                return NotFound("Publisher not found.");
            }
        }

        book.Isbn = model.Isbn ?? book.Isbn;
        book.Name = model.Name ?? book.Name;
        book.Description = model.Description ?? book.Description;
        book.Count = model.Count ?? book.Count;
        book.PublisherId = model.PublisherId ?? book.PublisherId;

        if (model.AuthorIds is not null)
        {
            if (model.AuthorIds.Count == 0)
            {
                return BadRequest("AuthorIds cannot be empty.");
            }

            var authors = await _dbContext.Authors.Where(a => model.AuthorIds.Contains(a.Id)).ToListAsync();
            if (authors.Count != model.AuthorIds.Count)
            {
                return NotFound("Author not found.");
            }

            // add those authors that are not in the old list
            foreach (var author in authors.Where(author => !book.Authors.Contains(author)))
            {
                book.Authors.Add(author);
            }

            // remove those authors that are not in the new list
            foreach (var author in book.Authors.ToList().Where(author => !authors.Contains(author)))
            {
                book.Authors.Remove(author);
            }
        }

        if (model.GenreIds is not null)
        {
            var genres = await _dbContext.Genres.Where(g => model.GenreIds.Contains(g.Id)).ToListAsync();
            if (genres.Count != model.GenreIds.Count)
            {
                return NotFound("Genre not found.");
            }

            // add those genres that are not in the old list
            foreach (var genre in genres.Where(genre => !book.Genres.Contains(genre)))
            {
                book.Genres.Add(genre);
            }

            // remove those genres that are not in the new list
            foreach (var genre in book.Genres.ToList().Where(genre => !genres.Contains(genre)))
            {
                book.Genres.Remove(genre);
            }
        }

        if (model.Price is not null)
        {
            var newPrice = new Price
            {
                BookPrice = model.Price.Value,
                ValidFrom = DateTime.Now,
            };

            book.Prices.Add(newPrice);
        }

        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    /// Delete book
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        var result = await _booksService.DeleteBookAsync(id);
        if (!result)
        {
            return NotFound("Book not found.");
        }

        await _booksService.SaveAsync();

        return Ok();
    }
}