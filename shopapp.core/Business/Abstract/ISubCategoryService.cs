using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.Business.Abstract;

public interface ISubCategoryService : IGenericService<SubCategory, SubCategoryDTO>
{
    Task<Response<SubCategoryDTO>> AddCheckUrlAndNameAsync(SubCategoryDTO entity);
}
