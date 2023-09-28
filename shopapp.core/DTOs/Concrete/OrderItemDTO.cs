using shopapp.core.DTOs.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.DTOs.Concrete;

public class OrderItemDTO:IDTO
{
	public int Id { get; set; }
	public int OrderId { get; set; }
	public OrderDTO Order { get; set; }
	public int ProductId { get; set; }
	public ProductDTO Product { get; set; }

	public double Price { get; set; }
	public int Quantity { get; set; }
}
