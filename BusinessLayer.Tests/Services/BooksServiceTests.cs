using BusinessLayer.Services;
using DataAccessLayer.Data;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class BooksServiceTests
{
    private DbContextOptions<BookHubDbContext> _dbContextOptions;
    private BookHubDbContext _dbContext;
    private BooksService _booksService;

    public BooksServiceTests()
    {
        _dbContextOptions = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        _dbContext = MockedDbContext.CreateFromOptions(_dbContextOptions);
        _booksService = new BooksService(_dbContext);
    }
    
    [Fact]
    public void Test()
    {
        Assert.True(true);
    }
}