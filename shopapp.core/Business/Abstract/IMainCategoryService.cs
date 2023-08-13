using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.Business.Abstract;

public interface IMainCategoryService : IGenericService<MainCategory, MainCategoryDTO>
{
    Task<Response<IEnumerable<MainCategoryDTO>>> GetAllWithCategoriAndSubCategories();
    Task<Response<IEnumerable<MainCategoryDTO>>> GetAllWithCategoriAndSubCategoriesAndBrands();
}
