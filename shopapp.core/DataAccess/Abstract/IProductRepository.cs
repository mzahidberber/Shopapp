using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.core.DataAccess.Abstract
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable<Product> WhereWithCategories(Expression<Func<Product, bool>> filter);
        Task<Product?> GetByIdWithCategoriesAsync(int id);
        Task<Product?> GetByUrlWithAttAsync(string url);

		Task<Product> GetByIdWithCategoriesAndImagesAsync(int id);
        IQueryable<Product> WherePage(int page, int pageSize, int sort, Expression<Func<Product, bool>>? filter = null);
        Task<int> GetCountByCategory(Expression<Func<Product, bool>>? filter = null);
        Task<Product> GetByIdWithAttAsync(int id);
        IQueryable<Product> GetWhereWithAtt(Expression<Func<Product, bool>> filter);
        IQueryable<Product> GetAllWithCategoriesAndBrands(Expression<Func<Product, bool>>? filter = null);


    }
}
