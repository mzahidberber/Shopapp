using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.core.DataAccess.Abstract
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable<Product> WhereWithCategories(Expression<Func<Product, bool>> filter);
        Task<Product> GetByIdWithCategoriesAsync(int id);
        IQueryable<Product> WherePage(int page, int pageSize, Expression<Func<Product, bool>>? filter = null);
        Task<int> GetCountByCategory(Expression<Func<Product, bool>>? filter = null);
    }
}
