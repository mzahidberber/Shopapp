using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.core.Business.Abstract
{
    public interface IProductService : IGenericService<Product, ProductDTO>
    {
        Task<Response<IEnumerable<ProductDTO>>> WhereWithCategories(Expression<Func<Product, bool>> predicate);
        Task<Response<ProductDTO>> GetByIdWithCategoriesAsync(int id);
        Task<Response<ProductDTO>> GetByIdWithCategoriesAndImagesAsync(int id);

        Task<Response<ProductDTOAndTotalCount>> WherePage(int page, int pageSize, int sort, Expression<Func<Product, bool>>? predicate = null);
        Task<Response<Dictionary<string, int>>> GetCountByCategory(string categoryUrl);

	}
}
