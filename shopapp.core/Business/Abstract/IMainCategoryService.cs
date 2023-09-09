using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.Business.Abstract;

public interface IMainCategoryService : IGenericService<MainCategory, MainCategoryDTO>
{
    Task<Response<IEnumerable<MainCategoryDTO>>> GetAllWithCategoriAndSubCategories();
    Task<Response<IEnumerable<MainCategoryDTO>>> GetAllWithCategoriAndSubCategoriesAndBrands();
    Task<Response<MainCategoryDTO>> AddCheckUrlAndNameAsync(MainCategoryDTO entity);
    Task<Response<NoDataDTO>> UpdateCheckUrlAndNameAsync(MainCategoryDTO entity, int id);
}
