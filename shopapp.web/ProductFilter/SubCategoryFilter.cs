using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.web.ProductFilter;

public class SubCategoryFilter : IFilter
{
    public List<string> Categories { get; set; }
    public SubCategoryFilter(List<string> categoeries)
    {
        Categories = categoeries;
    }
    public Expression<Func<Product, bool>> Expression()
    {
        return p => this.Categories.Any(x => x == p.SubCategory.Url);
    }
}
