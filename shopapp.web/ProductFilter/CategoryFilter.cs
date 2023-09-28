using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.web.ProductFilter;

public class CategoryFilter : IFilter
{
    public string Category { get; set; }
    public CategoryFilter(string category)
    {
        Category = category;
    }
    public Expression<Func<Product, bool>> Expression()
    {
        return p => this.Category == p.Category.Url || this.Category == p.MainCategory.Url || this.Category == p.SubCategory.Url;
    }
}
