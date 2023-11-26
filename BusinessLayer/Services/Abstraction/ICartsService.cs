using BusinessLayer.Models.Cart;

namespace BusinessLayer.Services.Abstraction
{
    public interface ICartsService : IBaseService
    {
        public Task<CartModel?> GetCart(int id);
        public Task<List<CartModel>> GetAllCarts();
        public Task<CartModel?> CreateCart(CreateCartModel createCartModel);
        public Task<bool> EditCart(int id, ICollection<int> bookIds, int? orderId);
        public Task<bool> DeleteCart(int id);
    }
}
