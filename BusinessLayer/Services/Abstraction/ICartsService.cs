using BusinessLayer.Models.Cart;
using DataAccessLayer.Models;

namespace BusinessLayer.Services.Abstraction
{
    public interface ICartsService : IBaseService
    {
        public Task<CartModel?> GetCart(int id);
        public Task<List<CartModel>> GetAllCarts();
        public Task<CartModel?> CreateCart(CreateCartModel createCartModel);
        public Task<bool> DeleteCart(int id);
        public Task<int> GetBookInCartCount(int cartId, int bookId);
        public Task AddBookToCart(int cartId, int bookId);
        public Task ChangeCartBookCount(int cartId, int bookId, int newCount);
        public Task RemoveBookFromCart(int cartId, int bookId);
    }
}
