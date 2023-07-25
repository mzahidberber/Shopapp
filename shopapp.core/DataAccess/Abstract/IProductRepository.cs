using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.core.DataAccess.Abstract
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        IQueryable<Product> WhereWithCategories(Expression<Func<Product, bool>> filter);
        Task<Product> GetByIdWithCategoriesAsync(int id);
    }
}
