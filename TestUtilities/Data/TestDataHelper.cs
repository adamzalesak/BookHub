using DataAccessLayer.Models;

namespace TestUtilities.Data;

public static class TestDataHelper
{
    public static List<Book> GetFakeBooks()
    {
        return new List<Book>();
    }

    public static List<User> GetFakeUsers()
    {
        return new List<User>
        {
            new()
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@email.com",
                Username = "johndoe",
            }
        };
    }
    
    public static List<Cart> GetFakeCarts()
    {
        return new List<Cart>
        {
            new()
            {
                Id = 1,
            },
            new()
            {
                Id = 2,
            },
        };
    }

    public static List<Order> GetFakeOrders()
    {
        return new List<Order>
        {
            new()
            {
                Id = 1,
                UserId = 1,
                CartId = 1,
                Address = "Test Address",
                Email = "test@email.com",
                Phone = 420605123456,
                TotalPrice = 123.45m,
            },
        };
    }
}