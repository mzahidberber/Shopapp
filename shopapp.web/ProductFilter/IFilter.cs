using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.web.ProductFilter;

public interface IFilter
{
    Expression<Func<Product, bool>> Expression();
}
