namespace shopapp.web.Models.Entity;

	public class SubCategoryModel
{
    public SubCategoryModel()
    {
        this.Brands = new List<BrandModel>();
        this.Products = new List<ProductModel>();
        this.SubCategoryFeatures = new List<SubCategoryFeatureModel>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public int CategoryId { get; set; }
    public CategoryModel Category { get; set; }

    public List<ProductModel> Products { get; set; }
    public List<BrandModel> Brands { get; set; }
    public List<SubCategoryFeatureModel> SubCategoryFeatures { get; set; }

}
