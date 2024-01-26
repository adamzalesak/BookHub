using BusinessLayer.Exceptions;
using BusinessLayer.Services.Abstraction;

namespace BusinessLayer.Facades;

public class OrderFacade : IOrderFacade
{
    private readonly IBooksService _booksService;
    private readonly ICartsService _cartsService;
    private readonly IUserService _userService;
    
    public OrderFacade(IBooksService booksService, ICartsService cartsService, IUserService userService)
    {
        _booksService = booksService;
        _cartsService = cartsService;
        _userService = userService;
    }
    
    public async Task<int> GetBookInCartCount(int cartId, int bookId)
    {
        return await _cartsService.GetBookInCartCount(cartId, bookId);
    }

    public async Task AddBookToCart(int cartId, int bookId)
    {
        var book = await _booksService.GetBookAsync(bookId);
        if (book == null)
        {
            throw new NotFoundException($"Book with bookId {bookId} not found.");
        }
        if (book.IsDeleted)
        {
            throw new InvalidOperationException($"Book with bookId {bookId} is deleted.");
        }

        if (book!.Count == 0)
        {
            throw new InvalidOperationException($"Book with bookId {bookId} is out of stock, cannot be added to cart.");
        }

        await _cartsService.AddBookToCart(cartId, bookId);

        // reserve the book for the user
        book.Count -= 1;
        await _booksService.SaveAsync();
    }

    public async Task ChangeCartBookCount(int cartId, int bookId, int newCount)
    {
        if (newCount < 1)
        {
            throw new ArgumentException("New book count cannot be negative or zero.");
        }
        
        var book = await _booksService.GetBookAsync(bookId);
        if (book == null)
        {
            throw new NotFoundException($"Book with bookId {bookId} not found.");
        }

        var bookInCartCount = await _cartsService.GetBookInCartCount(cartId, bookId);

        // optimization
        if (newCount == bookInCartCount)
        {
            return;
        }

        if (newCount > bookInCartCount && newCount - bookInCartCount > book!.Count)
        {
            throw new InvalidOperationException($"Changing cart count from {bookInCartCount} to {newCount} would cause the book with bookId {bookId} to get out of stock.");
        }

        await _cartsService.ChangeCartBookCount(cartId, bookId, newCount);

        // reserve the books for the user or increase the book count if newCount is smaller than bookInCartCount
        book!.Count -= newCount - bookInCartCount;
        await _booksService.SaveAsync();
    }

    public async Task RemoveBookFromCart(int cartId, int bookId)
    {
        var book = await _booksService.GetBookAsync(bookId);
        if (book == null)
        {
            throw new NotFoundException($"Book with bookId {bookId} not found.");
        }
        
        var bookInCartCount = await _cartsService.GetBookInCartCount(cartId, bookId);

        await _cartsService.RemoveBookFromCart(cartId, bookId);

        book.Count += bookInCartCount;
        await _booksService.SaveAsync();
    }
}