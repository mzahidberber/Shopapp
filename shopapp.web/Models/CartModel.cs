using shopapp.core.DTOs.Concrete;

namespace shopapp.web.Models
{
	public class CartModel
	{
        public int CartId { get; set; }
        public List<CartItemModel> CartItems { get; set; }
		public double TotalPrice()
		{
			return CartItems.Sum(i => i.Product.Price * i.Quantity);
		}
    }

	public class CartItemModel
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public ProductDTO Product { get; set; }
		public int CartId { get; set; }
		public CartDTO Cart { get; set; }
		public int Quantity { get; set; }
	}
}
