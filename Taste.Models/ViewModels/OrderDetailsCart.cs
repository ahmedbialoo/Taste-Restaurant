namespace Taste.Models.ViewModels
{
    public class OrderDetailsCart
    {
        public List<ShoppingCart>? CartList { get; set; }

        public OrderHeader OrderHeader { get; set; }
    }
}
