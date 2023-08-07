using shopapp.web.Models.Entity;

namespace shopapp.web.ViewModels
{
    public class ProductAndCategories
    {
        public PageInfo PageInfo { get; set; }
        public List<ProductModel> Products { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }

    public class PageInfo
    {
        public string Url { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
