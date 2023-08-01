using shopapp.web.Models.Entity;

namespace shopapp.web.Models
{
	public class CartModel
	{
		public int CartId { get; set; }
		public List<CartItemModel> CartItems { get; set; }
		public double TotalPrice()
		{
			return CartItems.Sum(i => i.Product.Price ?? 0 * i.Quantity);
		}
    }

	public class CartItemModel
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public ProductModel Product { get; set; }
		public int CartId { get; set; }
		public CartModel Cart { get; set; }
		public int Quantity { get; set; }
	}
}
