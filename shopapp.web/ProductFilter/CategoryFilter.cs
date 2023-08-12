using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.web.ProductFilter
{
    public class CategoryFilter : IFilter
    {
        public string[] Categories { get; set; }
        public CategoryFilter(string[] categoeries)
        {
            Categories = categoeries;
        }
        public Expression<Func<Product, bool>> Expression()
        {
            return p=> p.ProductCategories.Any(pr => this.Categories.Any(x => x == pr.Category.Url));
        }
    }
}
