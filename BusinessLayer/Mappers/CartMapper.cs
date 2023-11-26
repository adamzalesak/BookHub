using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models.Cart;
using DataAccessLayer.Models;

namespace BusinessLayer.Mappers
{
    [Mapper]
    public static partial class CartMapper
    {
        public static partial CartModel MapCartToCartModel(this Cart cart);
        public static partial List<CartModel> MapCartListToCartModelList(this List<Cart> carts);
        public static partial Cart MapCreateCartModelToCart(this CreateCartModel createCartModel);
    }
}
