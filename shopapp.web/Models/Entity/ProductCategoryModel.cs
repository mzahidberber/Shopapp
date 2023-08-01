

using shopapp.core.Entity.Concrete;

namespace shopapp.web.Models.Entity
{
	public class ProductCategoryModel
    {
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
    };
}
