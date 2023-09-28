using System.Text.Json.Serialization;

namespace shopapp.web.Models.Entity
{
    public class SubCategoryFeatureValueModel
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int SubCategoryFeatureId { get; set; }
        [JsonIgnore]
        public SubCategoryFeatureModel SubCategoryFeature { get; set; }

        public int ProductId { get; set; }
        [JsonIgnore]
        public ProductModel Product { get; set; }
    }
}
