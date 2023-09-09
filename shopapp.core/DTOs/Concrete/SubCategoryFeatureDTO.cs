using shopapp.core.DTOs.Abstract;

namespace shopapp.core.DTOs.Concrete;

public class SubCategoryFeatureDTO:IDTO
{
    public SubCategoryFeatureDTO()
    {
        this.SubCategoryFeatureValues = new List<SubCategoryFeatureValueDTO>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public int SubCategoryId { get; set; }
    public SubCategoryDTO SubCategory { get; set; }

    public List<SubCategoryFeatureValueDTO> SubCategoryFeatureValues { get; set; }
}
