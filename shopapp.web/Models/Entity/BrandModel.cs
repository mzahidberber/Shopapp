using shopapp.core.Entity.Concrete;

namespace shopapp.web.Models.Entity
{
    public class BrandModel
    {
        public BrandModel()
        {
            this.Products = new List<ProductModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategoryModel SubCategory { get; set; }

        public List<ProductModel> Products { get; set; }
    }
}
