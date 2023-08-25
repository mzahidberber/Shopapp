using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.web.ProductFilter;

public class SearchFilter : IFilter
{
    public string Search { get; set; }
    public SearchFilter(string search)
    {
        Search = search;
    }
    public Expression<Func<Product, bool>> Expression()
    {
        return x =>
        x.Brand.Name.ToLower().Contains(Search.ToLower()) ||
        x.Name.ToLower().Contains(Search.ToLower()) ||
        x.Category.Name.ToLower() == Search.ToLower() ||
        x.MainCategory.Name.ToLower() == Search.ToLower() ||
        x.SubCategory.Name.ToLower() == Search.ToLower();
    }
}
