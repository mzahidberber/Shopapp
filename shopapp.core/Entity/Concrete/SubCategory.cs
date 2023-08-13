using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete
{
    public class SubCategory : IEntity
    {
        public SubCategory()
        {
            this.Brands = new List<Brand>();
            this.Products = new List<Product>();
            this.SubCategoryFeatures = new List<SubCategoryFeature>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Product> Products { get; set; }
        public List<Brand> Brands { get; set; }
        public List<SubCategoryFeature> SubCategoryFeatures { get; set; }
    }
}
