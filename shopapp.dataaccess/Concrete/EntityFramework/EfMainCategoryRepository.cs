using Microsoft.EntityFrameworkCore;
using shopapp.core.Aspects.Logging;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
    [LogAspect(Priority = 1)]
    public class EfMainCategoryRepository : EfGenericRepository<MainCategory>, IMainCategoryRepository
    {
        public EfMainCategoryRepository(ShopContext context) : base(context)
        {
        }



        public IQueryable<MainCategory> GetAllWithCategoriesAndSubCategories()
        {
            return _dbSet
                .Include(x => x.Categories)
                .ThenInclude(x => x.SubCategories)
                .AsQueryable();
        }

        public IQueryable<MainCategory> GetAllWithCategoriesAndSubCategoriesAndBrands()
        {
            return _dbSet
                .Include(x => x.Categories)
                .ThenInclude(x => x.SubCategories)
                .ThenInclude(x => x.SubCategoryFeatures)
                .ThenInclude(x => x.SubCategoryFeatureValues)
                .Include(x => x.Categories)
                .ThenInclude(x => x.SubCategories)
                .ThenInclude(x => x.Brands)
                .AsQueryable();
        }
    }
}
