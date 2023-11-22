using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;

namespace BusinessLayer.Services;

public class BooksService : IBooksService
{
    private readonly BookHubBdContext _dbContext;
    
    public BooksService(BookHubBdContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> DeleteBookAsync(int bookId)
    {
        var book = await _dbContext.Books.FindAsync(bookId);
        if (book == null || book.IsDeleted)
        {
            return false;
        }

        book.IsDeleted = true;

        return true;
    }
    
    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}