using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete;

public class SubCategoryFeature:IEntity
{
    public SubCategoryFeature()
    {
        this.SubCategoryFeatureValues = new List<SubCategoryFeatureValue>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public int SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }

    public List<SubCategoryFeatureValue> SubCategoryFeatureValues { get; set; }
}
