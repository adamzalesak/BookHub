using System.Globalization;
using System.Linq.Expressions;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
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
        public async Task<ActionResult<ICollection<BookModel>>> GetBooks(
            [FromQuery] string? name,
            [FromQuery] string? description,
            [FromQuery] int? authorId,
            [FromQuery] string? authorName,
            [FromQuery] int? publisherId,
            [FromQuery] string? publisherName,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int? page,
            [FromQuery] int? pageSize,
            [FromQuery] string? orderBy,
            [FromQuery] bool? orderDesc
        )
        {
            if (authorId != null)
            {
                var author = await _dbContext.Authors.FindAsync(authorId);
                if (author == null)
                {
                    return NotFound("Author not found.");
                }
            }

            if (publisherId != null)
            {
                var publisher = await _dbContext.Publishers.FindAsync(publisherId);
                if (publisher == null)
                {
                    return NotFound("Publisher not found.");
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
            
            Expression<Func<Book, bool>> pricePredicate = book =>
                (minPrice == null || book.Prices.MaxBy(p => p.ValidFrom).BookPrice >= minPrice) && (maxPrice == null || book.Prices.MaxBy(p => p.ValidFrom).BookPrice <= maxPrice);

            var booksQuery = _dbContext.Books
                .Where(namePredicate)
                .Where(descriptionPredicate)
                .Where(authorIdPredicate)
                .Where(authorNamePredicate)
                .Where(publisherIdPredicate)
                .Where(publisherNamePredicate)
                .Where(pricePredicate);

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                Expression<Func<Book, string?>> orderFunction = book =>
                    orderBy == "name" ? book.Name :
                    orderBy == "description" ? book.Description :
                    orderBy == "price" ? book.Prices.MaxBy(p => p.ValidFrom).BookPrice.ToString(CultureInfo.InvariantCulture) :
                    orderBy == "author" ? book.Authors.Select(a => a.Name).FirstOrDefault() :
                    orderBy == "publisher" ? book.Publisher.Name :
                    book.Name;

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
                .Select(book => new BookModel
                {
                    Id = book.Id,
                    Isbn = book.Isbn,
                    Name = book.Name,
                    Description = book.Description,
                    Price = book.Prices.MaxBy(p => p.ValidFrom).BookPrice,
                    Authors = book.Authors.Select(a => a.Name).ToList(),
                    Publisher = book.Publisher.Name,
                }).ToListAsync();


            return books;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel>> GetBook(int id)
        {
            var book = await _dbContext.Books
                .Where(b => b.Id == id)
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .Select(book => new BookModel
                {
                    Id = book.Id,
                    Isbn = book.Isbn,
                    Name = book.Name,
                    Description = book.Description,
                    Price = book.Prices.MaxBy(p => p.ValidFrom).BookPrice,
                    Authors = book.Authors.Select(a => a.Name).ToList(),
                    Publisher = book.Publisher.Name,
                }).FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(book);
        }

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

            var newBook = new Book
            {
                Isbn = model.Isbn,
                Name = model.Name,
                Description = model.Description,
                Authors = authors,
                PublisherId = model.PublisherId,
            };

            await _dbContext.Books.AddAsync(newBook);
            await _dbContext.SaveChangesAsync();

            var newBookModel = new BookModel
            {
                Id = newBook.Id,
                Isbn = newBook.Isbn,
                Name = newBook.Name,
                Description = newBook.Description,
                Price = newBook.Prices.MaxBy(p => p.ValidFrom).BookPrice,
                Authors = newBook.Authors.Select(a => a.Name).ToList(),
                Publisher = newBook.Publisher.Name,
            };

            return Created($"/books/{newBookModel.Id}", newBookModel);
        }

        [HttpPut]
        public async Task<ActionResult> EditBook(EditBookModel model)
        {
            var book = await _dbContext.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Id == model.Id);
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
                foreach (var author in authors)
                {
                    if (!book.Authors.Contains(author))
                    {
                        book.Authors.Add(author);
                    }
                }

                // remove those authors that are not in the new list
                foreach (var author in book.Authors.ToList())
                {
                    if (!authors.Contains(author))
                    {
                        book.Authors.Remove(author);
                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}