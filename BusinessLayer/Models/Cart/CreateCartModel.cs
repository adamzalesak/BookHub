namespace BusinessLayer.Models.Cart
{
    public class CreateCartModel
    {
        public List<int> CartItemIds { get; set; }
        public int? OrderId { get; set; }
    }
}
