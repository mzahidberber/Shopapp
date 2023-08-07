using shopapp.core.DTOs.Abstract;

namespace shopapp.core.DTOs.Concrete
{
    public class ProductDTO : IDTO
    {
        public ProductDTO()
        {
            this.ProductCategories = new List<ProductCategoryDTO>();
            this.Images = new List<ImageDTO>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string HomeImageUrl { get; set; }
        public int Stock { get; set; }
        public bool IsApprove { get; set; }
        public bool IsHome { get; set; }
        public List<ProductCategoryDTO> ProductCategories { get; set; }

        public List<ImageDTO> Images { get; set; }
    }
}
