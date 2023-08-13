using shopapp.core.Entity.Concrete;

namespace shopapp.core.DataAccess.Abstract
{
    public interface IMainCategoryRepository : IGenericRepository<MainCategory>
    {
        IQueryable<MainCategory> GetAllWithCategoriesAndSubCategories();
        IQueryable<MainCategory> GetAllWithCategoriesAndSubCategoriesAndBrands();
    }
}
