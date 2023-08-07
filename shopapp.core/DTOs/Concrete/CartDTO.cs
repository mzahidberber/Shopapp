using shopapp.core.DTOs.Abstract;

namespace shopapp.core.DTOs.Concrete
{
    public class CartDTO : IDTO
    {
        public CartDTO()
        {
            this.CartItems = new List<CartItemDTO>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemDTO> CartItems { get; set; }

        public double TotalPrice()
        {
            return CartItems.Sum(i => i.Product.Price * i.Quantity);
        }
    }
}
