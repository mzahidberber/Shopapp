using shopapp.core.Entity.Concrete;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace shopapp.web.ProductFilter
{
    public class BrandFilter : IFilter
    {
        public List<string> Brands { get; set; }
        public BrandFilter(List<string> brandFilter)
        {
            Brands = brandFilter;
        }
        public Expression<Func<Product, bool>> Expression()
        {
            return p => this.Brands.Any(x=> x == p.Brand.Url);

        }
    }
}
