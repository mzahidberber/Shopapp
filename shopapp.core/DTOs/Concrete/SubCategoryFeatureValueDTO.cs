using shopapp.core.DTOs.Abstract;

namespace shopapp.core.DTOs.Concrete;

public class SubCategoryFeatureValueDTO:IDTO
{
    public int Id { get; set; }
    public string Value { get; set; }

    public int SubCategoryFeatureId { get; set; }
    public SubCategoryFeatureDTO SubCategoryFeature { get; set; }

    public int ProductId { get; set; }
    public ProductDTO Product { get; set; }
}
