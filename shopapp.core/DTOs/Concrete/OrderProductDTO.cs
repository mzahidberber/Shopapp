namespace shopapp.core.DTOs.Concrete;

public class OrderProductDTO
{
	public int OrderId { get; set; }
	public OrderDTO Order { get; set; }
	public int ProductId { get; set; }
	public ProductDTO Product { get; set; }
	public int Quantity { get; set; }
}
