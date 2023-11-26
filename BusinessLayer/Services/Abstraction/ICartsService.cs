using BusinessLayer.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
