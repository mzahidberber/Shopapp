using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete
{
    public class Product : IEntity
    {
        public Product()
        {
            this.ProductCategories = new List<ProductCategory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Stock { get; set; }
        public bool IsApprove { get; set; }
        public bool IsHome { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
