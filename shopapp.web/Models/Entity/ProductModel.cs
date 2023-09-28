using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.ComponentModel.DataAnnotations;

namespace shopapp.web.Models.Entity
{
    public class ProductModel
    {
        public ProductModel()
        {
            this.Images = new List<ImageModel>();
            this.SubCategoryFeatureValues = new List<SubCategoryFeatureValueModel>();
            this.OrderItems = new List<OrderItemModel>();

        }

        public int Id { get; set; }

        [Display(Name = "Name", Prompt = "Enter product name")]
        [Required(ErrorMessage = "Name Required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Name 5 -100 char")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url Required")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Price Required")]
        [Range(0, 100000, ErrorMessage = "Price 1-100000")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        public string? HomeImageUrl { get; set; }


        public bool IsApprove { get; set; }
        public bool IsHome { get; set; }
        public int Stock { get; set; }
        public List<ImageModel> Images { get; set; }

        public int BrandId { get; set; }
        public BrandModel? Brand { get; set; }

        public int CategoryId { get; set; }
        public CategoryModel? Category { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategoryModel? SubCategory { get; set; }

        public int MainCategoryId { get; set; }
        public MainCategoryModel? MainCategory { get; set; }

		public List<OrderItemModel> OrderItems { get; set; }

		public List<SubCategoryFeatureValueModel> SubCategoryFeatureValues { get; set; }
    }
}
