using shopapp.web.Models.Entity;

namespace shopapp.web.Models.Cart;

public class CartModel
{
    public CartModel()
    {
        this.CartItems = new List<CartItemModel>();
    }
    public int CartId { get; set; }
    public List<CartItemModel> CartItems { get; set; }
    public double TotalPrice()
    {
        return CartItems.Sum(i => (i.Product.Price) * i.Quantity);
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
