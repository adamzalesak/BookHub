using BusinessLayer.Exceptions;
using BusinessLayer.Mappers;
using BusinessLayer.Models.Cart;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class CartsService : ICartsService
    {
        private readonly BookHubDbContext _dbContext;

        public CartsService(BookHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<CartModel?> GetCart(int id)
        {
            var cart = await GetCartObject(id);
            if (cart == null)
            {
                return null;
            }
            return cart.MapToCartModel();
        }
        
        public async Task<List<CartModel>> GetAllCarts()
        {
            var carts = await _dbContext.Carts
                .Include(c => c.CartItems)
                .Include(c => c.Order)
                .ToListAsync();
            return carts.MapToCartModelList();
        }

        public async Task<CartModel?> CreateCart(CreateCartModel createCartModel)
        {
            var newCart = new Cart
            {
                CartItems = await _dbContext.CartItems.Where(ci => createCartModel.CartItemIds.Contains(ci.Id)).ToListAsync(),
                Order = await _dbContext.Orders.SingleOrDefaultAsync(o => o.Id == createCartModel.OrderId),
            };
            var cart = await _dbContext.Carts.AddAsync(newCart);
            await SaveAsync();
            return cart.Entity.MapToCartModel();
        }

        public async Task<bool> DeleteCart(int id)
        {
            var cart = await GetCartObject(id);
            if (cart == null)
            {
                throw new NotFoundException($"Cart with cartId {id} not found");
            }
            _dbContext.Carts.Remove(cart);
            await SaveAsync();
            return true;
        }

        public async Task<int> GetBookInCartCount(int cartId, int bookId)
        {
            var cartItem = await GetCartItem(cartId, bookId);
            if (cartItem == null)
            {
                throw new NotFoundException($"Book with bookId {bookId} not found in cart with cartId ${cartId} or the cart does not exist.");
            }

            return cartItem.Count;
        }

        public async Task AddBookToCart(int cartId, int bookId)
        {
            // check to avoid putting inconsistent data into database
            await CheckBookExistence(bookId);
            
            var cart = await GetCartObject(cartId);
            if (cart == null)
            {
                throw new NotFoundException($"Cart with cartId {cartId} not found.");
            }

            var cartItemWithBookId = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
            
            // if cartItem with given bookId already exists
            if (cartItemWithBookId != null)
            {
                throw new InvalidOperationException($"Book with bookId {bookId} is already in cart with cartId {cartId}");
            }

            // else create new cart item
            var newCartItem = new CartItem()
            {
                CartId = cartId,
                BookId = bookId,
                Count = 1
            };

            cart.CartItems.Add(newCartItem);
            await SaveAsync();
        }

        public async Task ChangeCartBookCount(int cartId, int bookId, int newCount)
        {
            if (newCount < 1)
            {
                throw new ArgumentException("New book count cannot be negative or zero.");
            }
            
            var cartItem = await GetCartItem(cartId, bookId);
            if (cartItem == null)
            {
                throw new NotFoundException($"Book with bookId {bookId} not found in cart with cartId ${cartId} or the cart does not exist.");
            }

            cartItem.Count = newCount;
            await SaveAsync();
        }
        
        public async Task RemoveBookFromCart(int cartId, int bookId)
        {
            var cart = await GetCartObject(cartId);
            if (cart == null)
            {
                throw new NotFoundException($"Cart with cartId {cartId} not found.");
            }

            var cartItemWithBookId = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
            if (cartItemWithBookId == null)
            {
                throw new NotFoundException($"Book with bookId {bookId} not found in cart with cartId ${cartId}");
            }

            cart.CartItems.Remove(cartItemWithBookId);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Cart?> GetCartObject(int id)
        {
            return await _dbContext.Carts
                .Where(c => c.Id == id)
                .Include(c => c.CartItems)
                .Include(c => c.Order)
                .FirstOrDefaultAsync();
        }

        private async Task CheckBookExistence(int bookId)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                throw new NotFoundException($"Book with bookId {bookId} not found.");
            }
            if (book.IsDeleted)
            {
                throw new InvalidOperationException($"Book with bookId {bookId} is deleted.");
            }
        }

        private async Task<CartItem?> GetCartItem(int cartId, int bookId)
        {
            return await _dbContext.Carts
                .Where(c => c.Id == cartId)
                .SelectMany(c => c.CartItems)
                .FirstOrDefaultAsync(ci => ci.BookId == bookId);
        }
    }
}
