using shopapp.core.DTOs.Abstract;

namespace shopapp.core.DTOs.Concrete
{
    public class CartItemDTO:IDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int CartId { get; set; }
        public CartDTO Cart { get; set; }
        public int Quantity { get; set; }
    }
}
