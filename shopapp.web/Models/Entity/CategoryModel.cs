using shopapp.core.Entity.Concrete;

namespace shopapp.web.Models.Entity
{
	public class CategoryModel
    {
		public CategoryModel()
        {
            this.ProductCategories = new List<ProductCategoryModel>();
        }
		public int Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }

		public List<ProductCategoryModel> ProductCategories { get; set; }
    }
}
