namespace shopapp.web.Models.Checkout;

public class CheckoutProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}
