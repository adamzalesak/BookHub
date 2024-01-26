namespace BusinessLayer.Facades;

public interface IOrderFacade
{
    public Task<int> GetBookInCartCount(int cartId, int bookId);
    public Task AddBookToCart(int cartId, int bookId);
    public Task ChangeCartBookCount(int cartId, int bookId, int newCount);
    public Task RemoveBookFromCart(int cartId, int bookId);
}