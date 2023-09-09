using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.Business.Abstract;

public interface IBrandService : IGenericService<Brand, BrandDTO>
{
    Task<Response<BrandDTO>> AddCheckUrlAndNameAsync(BrandDTO entity);
}
