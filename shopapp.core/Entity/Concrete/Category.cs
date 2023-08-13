using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete
{
    public class Category : IEntity
    {
        public Category()
        {
            this.Products = new List<Product>();
            this.SubCategories = new List<SubCategory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; }

        public List<Product> Products { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}
