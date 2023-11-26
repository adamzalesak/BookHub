using DataAccessLayer.Data;
using EntityFrameworkCore.Testing.NSubstitute.Helpers;
using Microsoft.EntityFrameworkCore;
using TestUtilities.Data;

namespace TestUtilities.MockedObjects;

public static class MockedDbContext
{
    public static string RandomDBName => Guid.NewGuid().ToString();

    public static DbContextOptions<BookHubBdContext> GenerateNewInMemoryDbContextOptions()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BookHubBdContext>()
            .UseInMemoryDatabase(RandomDBName)
            .Options;

        return dbContextOptions;
    }

    public static BookHubBdContext CreateFromOptions(DbContextOptions<BookHubBdContext> options)
    {
        var dbContextToMock = new BookHubBdContext(options);

        var dbContext = new MockedDbContextBuilder<BookHubBdContext>()
            .UseDbContext(dbContextToMock)
            .UseConstructorWithParameters(options)
            .MockedDbContext;

        PrepareData(dbContext);

        return dbContext;
    }

    public static void PrepareData(BookHubBdContext dbContext)
    {
        dbContext.Books.AddRange(TestDataHelper.GetFakeBooks());
        dbContext.Users.AddRange(TestDataHelper.GetFakeUsers());
        dbContext.Orders.AddRange(TestDataHelper.GetFakeOrders());
        
        dbContext.SaveChanges();
    }
}
