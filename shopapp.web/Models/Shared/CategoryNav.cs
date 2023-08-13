using shopapp.web.Models.Entity;

namespace shopapp.web.Models.Shared;

public class CategoryNav
{
    public List<CategoryModel> Categories { get; set; }
    public List<MainCategoryModel> MainCategories { get; set; }
}
