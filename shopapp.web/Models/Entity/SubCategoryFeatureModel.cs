using shopapp.core.DTOs.Concrete;
using System.Text.Json.Serialization;

namespace shopapp.web.Models.Entity
{
    public class SubCategoryFeatureModel
    {
        public SubCategoryFeatureModel()
        {
            this.SubCategoryFeatureValues = new List<SubCategoryFeatureValueModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        [JsonIgnore]
        public SubCategoryModel Category { get; set; }

        public List<SubCategoryFeatureValueModel> SubCategoryFeatureValues { get; set; }
    }
}
