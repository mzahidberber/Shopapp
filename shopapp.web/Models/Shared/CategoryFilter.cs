using shopapp.web.Models.Entity;

namespace shopapp.web.Models.Shared
{
    public class CategoryInfo
    {
        public List<CategoryModel> Categories { get; set; }
        public string[] SelectedCategories { get; set; }
    }
}
