using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete
{
    public class Category : IEntity
    {
        public Category()
        {
            this.ProductCategories = new List<ProductCategory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}
