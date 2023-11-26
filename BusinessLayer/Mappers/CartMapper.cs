using BusinessLayer.Models.Cart;
using DataAccessLayer.Models;
using Riok.Mapperly.Abstractions;

namespace BusinessLayer.Mappers
{
    [Mapper]
    public static partial class CartMapper
    {
        public static partial CartModel MapToCartModel(this Cart cart);
        public static partial List<CartModel> MapToCartModelList(this ICollection<Cart> carts);
    }
}
