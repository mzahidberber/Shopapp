using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete
{
    public class Cart : IEntity
    {
        public Cart()
        {
            this.CartItems = new List<CartItem>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
