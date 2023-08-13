using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.web.Models.Entity
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            this.Products = new List<ProductModel>();
            this.SubCategories = new List<SubCategoryModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int MainCategoryId { get; set; }
        public MainCategoryModel MainCategory { get; set; }

        public List<ProductModel> Products { get; set; }
        public List<SubCategoryModel> SubCategories { get; set; }
    }
}
