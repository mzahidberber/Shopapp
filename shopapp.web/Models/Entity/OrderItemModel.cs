namespace shopapp.web.Models.Entity;

public class OrderItemModel
{
	public int Id { get; set; }
	public int OrderId { get; set; }
	public OrderModel Order { get; set; }
	public int ProductId { get; set; }
	public ProductModel Product { get; set; }

	public double Price { get; set; }
	public int Quantity { get; set; }
}
