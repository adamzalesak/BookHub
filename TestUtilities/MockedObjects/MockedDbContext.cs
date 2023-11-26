using DataAccessLayer.Data;
using EntityFrameworkCore.Testing.NSubstitute.Helpers;
using Microsoft.EntityFrameworkCore;
using TestUtilities.Data;

namespace TestUtilities.MockedObjects;

public static class MockedDbContext
{
    private static string RandomDbName => Guid.NewGuid().ToString();

    public static DbContextOptions<BookHubDbContext> GenerateNewInMemoryDbContextOptions()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BookHubDbContext>()
            .UseInMemoryDatabase(RandomDbName)
            .Options;

        return dbContextOptions;
    }

    public static BookHubDbContext CreateFromOptions(DbContextOptions<BookHubDbContext> options)
    {
        var dbContextToMock = new BookHubDbContext(options);

        var dbContext = new MockedDbContextBuilder<BookHubDbContext>()
            .UseDbContext(dbContextToMock)
            .UseConstructorWithParameters(options)
            .MockedDbContext;

        PrepareData(dbContext);

        return dbContext;
    }

    private static void PrepareData(BookHubDbContext dbContext)
    {
        dbContext.Books.AddRange(TestDataHelper.GetFakeBooks());
        dbContext.Users.AddRange(TestDataHelper.GetFakeUsers());
        dbContext.Carts.AddRange(TestDataHelper.GetFakeCarts());
        dbContext.Orders.AddRange(TestDataHelper.GetFakeOrders());
        
        dbContext.SaveChanges();
    }
}
