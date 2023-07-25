using shopapp.core.DTOs.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.DTOs.Concrete
{
    public class CategoryDTO:IDTO
    {
        public CategoryDTO()
        {
            this.ProductCategories = new List<ProductCategory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}
