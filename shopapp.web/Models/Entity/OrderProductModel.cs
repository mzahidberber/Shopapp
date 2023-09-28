using shopapp.core.DTOs.Concrete;

namespace shopapp.web.Models.Entity
{
	public class OrderProductModel
	{
		public int OrderId { get; set; }
		public OrderModel Order { get; set; }
		public int ProductId { get; set; }
		public ProductModel Product { get; set; }
		public int Quantity { get; set; }
	}
}
