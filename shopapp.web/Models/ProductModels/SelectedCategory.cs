using shopapp.web.Models.Entity;

namespace shopapp.web.Models.ProductModels;

public class SelectedCategoryModel
{
    public MainCategoryModel? MainCategory { get; set; }
    public CategoryModel? Category { get; set; }
    public SubCategoryModel? SubCategory { get; set; }
    public IEnumerable<BrandModel>? Brands { get; set; }
    public int CategoryType { get; set; }
}
