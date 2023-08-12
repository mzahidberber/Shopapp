using shopapp.web.Models.Entity;
using shopapp.web.Models.Shared;

namespace shopapp.web.ViewModels
{
    public class ProductInfo
    {
        public PageInfo PageInfo { get; set; }
        public List<ProductModel> Products { get; set; }
    }

    public class SelectedInfo
    {
        public string? Search { get; set; }
        public int Sort { get; set; }
        public string? Price { get; set; }
        public string[] Categories { get; set; }
        public int Page { get; set; }
    }

    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int MostPrice { get; set; }
        public string Url { get; set; }
        public SelectedInfo Selected { get; set; }

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
