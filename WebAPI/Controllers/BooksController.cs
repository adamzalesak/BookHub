using System.Globalization;
using System.Linq.Expressions;
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

    public BooksController(BookHubBdContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Get all books
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ICollection<BookModel>>> GetBooks(
        [FromQuery] string? name,
        [FromQuery] string? description,
        [FromQuery] int? authorId,
        [FromQuery] string? authorName,
        [FromQuery] int? publisherId,
        [FromQuery] string? publisherName,
        [FromQuery] int? genreId,
        [FromQuery] string? genreName,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int? page,
        [FromQuery] int? pageSize,
        [FromQuery] string? orderBy,
        [FromQuery] bool? orderDesc
    )
    {
        if (authorId is not null)
        {
            var author = await _dbContext.Authors.FindAsync(authorId);
            if (author == null)
            {
                return NotFound("Author not found.");
            }
        }

        if (publisherId is not null)
        {
            var publisher = await _dbContext.Publishers.FindAsync(publisherId);
            if (publisher == null)
            {
                return NotFound("Publisher not found.");
            }
        }

        if (genreId is not null)
        {
            var genre = await _dbContext.Genres.FindAsync(genreId);
            if (genre == null)
            {
                return NotFound("Genre not found.");
            }
        }

        Expression<Func<Book, bool>> namePredicate = string.IsNullOrWhiteSpace(name)
            ? book => true
            : book => book.Name.ToLower().Contains(name.ToLower());

        Expression<Func<Book, bool>> descriptionPredicate = string.IsNullOrWhiteSpace(description)
            ? book => true
            : book => book.Description.ToLower().Contains(description.ToLower());

        Expression<Func<Book, bool>> authorIdPredicate = authorId == null
            ? book => true
            : book => book.Authors.Any(a => a.Id == authorId);

        Expression<Func<Book, bool>> authorNamePredicate = string.IsNullOrWhiteSpace(authorName)
            ? book => true
            : book => book.Authors.Any(a => a.Name.ToLower().Contains(authorName.ToLower()));

        Expression<Func<Book, bool>> publisherIdPredicate = publisherId == null
            ? book => true
            : book => book.Publisher.Id == publisherId;

        Expression<Func<Book, bool>> publisherNamePredicate = string.IsNullOrWhiteSpace(publisherName)
            ? book => true
            : book => book.Publisher.Name.ToLower().Contains(publisherName.ToLower());

        Expression<Func<Book, bool>> genreIdPredicate = genreId == null
            ? book => true
            : book => book.Genres.Any(g => g.Id == genreId);

        Expression<Func<Book, bool>> genreNamePredicate = string.IsNullOrWhiteSpace(genreName)
            ? book => true
            : book => book.Genres.Any(g => g.Name.ToLower().Contains(genreName.ToLower()));

        Expression<Func<Book, bool>> pricePredicate = book =>
            (minPrice == null || book.Prices.OrderByDescending(p => p.ValidFrom).First().BookPrice >= minPrice) &&
            (maxPrice == null || book.Prices.OrderByDescending(p => p.ValidFrom).First().BookPrice <= maxPrice);

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

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            Expression<Func<Book, string?>> orderFunction = book =>
                orderBy == "name" ? book.Name :
                orderBy == "description" ? book.Description :
                orderBy == "price" ? book.Prices.OrderByDescending(p => p.ValidFrom).First().BookPrice
                    .ToString(CultureInfo.InvariantCulture) :
                orderBy == "publisher" ? book.Publisher.Name : book.Name;

            if (orderDesc == true)
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
            .Skip((page ?? 0) * (pageSize ?? 10))
            .Take(pageSize ?? 10)
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
        var book = await _dbContext.Books.FindAsync(id);
        if (book == null || book.IsDeleted)
        {
            return NotFound("Book not found.");
        }

        book.IsDeleted = true;
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}