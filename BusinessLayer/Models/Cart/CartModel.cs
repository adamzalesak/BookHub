namespace BusinessLayer.Models.Cart;

public class CartModel
{
    public int Id { get; set; }
    public List<int> BookIds { get; set; }
    public int? OrderId { get; set; }
}