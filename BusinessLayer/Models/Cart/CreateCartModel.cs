namespace BusinessLayer.Models.Cart
{
    public class CreateCartModel
    {
        public List<int> BookIds { get; set; }
        public int? OrderId { get; set; }
    }
}
