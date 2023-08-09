using shopapp.web.Models.Entity;
using shopapp.web.Models.Shared;

namespace shopapp.web.ViewModels
{
    public class ProductList
    {
        public PageInfo PageInfo { get; set; }

        public int MostPrice { get; set; }
        public List<ProductModel> Products { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }

    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
