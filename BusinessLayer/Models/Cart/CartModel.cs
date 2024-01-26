using DataAccessLayer.Models;

namespace BusinessLayer.Models.Cart;

public class CartModel
{
    public int Id { get; set; }
    public List<CartItem> CartItems { get; set; }
    public int? OrderId { get; set; }
}