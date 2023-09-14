using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.Business.Abstract;

public interface IBrandService : IGenericService<Brand, BrandDTO>
{
    Task<Response<BrandDTO>> AddCheckUrlAndNameAsync(BrandDTO entity);
    Task<Response<NoDataDTO>> UpdateCheckUrlAndNameAsync(BrandDTO entity, int id);
    Task<Response<BrandProductCount>> GetProductCountAsync(int id);
    Task<Response<NoDataDTO>> UpdateLastSubCategory(BrandDTO entity, int id);
}
