using shopapp.core.Entity.Concrete;
using System.ComponentModel.DataAnnotations;

namespace shopapp.web.Models.Entity
{
	public class ProductModel
    {
		public ProductModel()
        {
            this.ProductCategories = new List<ProductCategoryModel>();
        }

		public int Id { get; set; }
        
        [Display(Name="Name",Prompt ="Enter product name")]
        [Required(ErrorMessage = "Name Required")]
        [StringLength(100,MinimumLength =5,ErrorMessage ="Name 5 -100 char")]
		public string Name { get; set; }

        [Required(ErrorMessage = "Url Required")]
		public string Url { get; set; }

        [Required(ErrorMessage = "Price Required")]
        [Range(0,10000,ErrorMessage ="Price 1-10000")]
		public double? Price { get; set; }

        [Required(ErrorMessage = "Description Required")]
		public string Description { get; set; }

		public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Stock Required")]
		public int? Stock { get; set; }

        public bool IsApprove { get; set; }
        public bool IsHome { get; set; }
        public List<ProductCategoryModel> ProductCategories { get; set; }
    }
}
