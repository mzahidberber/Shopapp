using shopapp.core.DTOs.Concrete;

namespace shopapp.web.ViewModels
{
    public class ProductAndCategories
    {
        public List<ProductDTO> Products { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
