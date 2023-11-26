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
        private readonly BookHubBdContext _dbContext;

        public CartsService(BookHubBdContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CartModel?> CreateCart(CreateCartModel createCartModel)
        {
            var newCart = new Cart
            {
                Books = await _dbContext.Books.Where(b => createCartModel.BookIds.Contains(b.Id)).ToListAsync(),
                Order = await _dbContext.Orders.SingleOrDefaultAsync(c => c.Id == createCartModel.OrderId),
            };
            var cart = await _dbContext.Carts.AddAsync(newCart);
            await SaveAsync();
            return cart.Entity.MapToCartModel();
        }

        public async Task<bool> DeleteCart(int id)
        {
            var cart = await this.GetCartObject(id);
            if (cart == null)
            {
                return false;
            }
            _dbContext.Carts.Remove(cart);
            await SaveAsync();
            return true;
        }

        public async Task<bool> EditCart(int id, ICollection<int> bookIds, int? orderId)
        {
            var cart = await this.GetCartObject(id);
            if (cart == null)
            {
                return false;

            }
            cart.Books = await _dbContext.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();
            cart.Order = await _dbContext.Orders.SingleOrDefaultAsync(c => c.Id == orderId);
            await SaveAsync();
            return true;
        }

        public async Task<List<CartModel>> GetAllCarts()
        {
            var carts = await _dbContext.Carts
                .Include(c => c.Books)
                .Include(c => c.Order)
                .ToListAsync();
            return carts.MapToCartModelList();
        }

        public async Task<CartModel?> GetCart(int id)
        {
            var cart = await this.GetCartObject(id);
            if (cart == null)
            {
                return null;
            }
            return cart.MapToCartModel();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Cart?> GetCartObject(int id)
        {
            return await _dbContext.Carts
                .Include(c => c.Books)
                .Include(c => c.Order)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
