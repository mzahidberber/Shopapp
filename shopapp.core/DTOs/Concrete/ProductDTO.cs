using shopapp.core.DTOs.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.DTOs.Concrete
{
    public class ProductDTO : IDTO
    {
        public ProductDTO()
        {
            this.Images = new List<ImageDTO>();
            this.SubCategoryFeatureValues = new List<SubCategoryFeatureValueDTO>();
            this.OrderItems=new List<OrderItemDTO>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string HomeImageUrl { get; set; }
        public bool IsApprove { get; set; }
        public bool IsHome { get; set; }
		public int Stock { get; set; }
		public int BrandId { get; set; }
        public BrandDTO Brand { get; set; }

        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }

		public int SubCategoryId { get; set; }
		public SubCategoryDTO SubCategory { get; set; }

		public int MainCategoryId { get; set; }
        public MainCategoryDTO MainCategory { get; set; }

        public List<ImageDTO> Images { get; set; }

		public List<OrderItemDTO> OrderItems { get; set; }


		public List<SubCategoryFeatureValueDTO> SubCategoryFeatureValues { get; set; }
    }
}
