using System.Text.Json.Serialization;

namespace shopapp.web.Models.Entity
{
    public class MainCategoryModel
    {
        public MainCategoryModel()
        {
            this.Categories = new List<CategoryModel>();
            this.Products = new List<ProductModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        [JsonIgnore]
        public List<ProductModel> Products { get; set; }

        public List<CategoryModel> Categories { get; set; }
    }
}
