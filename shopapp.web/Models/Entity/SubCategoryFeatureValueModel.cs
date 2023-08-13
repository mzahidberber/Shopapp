using shopapp.core.Entity.Concrete;

namespace shopapp.web.Models.Entity
{
    public class SubCategoryFeatureValueModel
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int SubCategoryFeatureId { get; set; }
        public SubCategoryFeatureModel SubCategoryFeature { get; set; }

        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
    }
}
