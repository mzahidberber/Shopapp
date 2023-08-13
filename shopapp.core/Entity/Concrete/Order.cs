using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete
{
    public class Order : IEntity
    {
        public Order()
        {
            this.Products = new List<Product>();

        }
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; }
        public List<Product> Products { get; set; }
    }
}
