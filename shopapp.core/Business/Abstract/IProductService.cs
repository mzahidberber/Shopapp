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
        Task<Response<ProductDTO>> GetByIdWithAttsAsync(int id);
        Task<Response<ProductDTO>> GetByUrlWithAttsAsync(string url);

        Task<Response<IEnumerable<ProductDTO>>> WhereWithAtt(Expression<Func<Product, bool>> predicate);

        Task<Response<ProductDTOAndTotalCount>> WherePage(string? category, int page, int pageSize, int sort, Expression<Func<Product, bool>>? predicate = null);
        Task<Response<Dictionary<string, int>>> GetCountByCategory(string categoryUrl);
        Response<IsUrlDTO> IsUrl(string url);
        Task<Response<NoDataDTO>> ChangeApprove(int id, bool isApprove);
        Task<Response<NoDataDTO>> ChangeHome(int id, bool isHome);
        Task<Response<ProductDTO>> AddCheckUrlAsync(ProductDTO entity);
        Task<Response<IEnumerable<ProductDTO>>> GetAllWithCategoriesAndBrandAsync();


    }
}
