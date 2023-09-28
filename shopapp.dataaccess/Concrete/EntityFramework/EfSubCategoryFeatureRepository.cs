using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework;

public class EfSubCategoryFeatureRepository : EfGenericRepository<SubCategoryFeature>, ISubCategoryFeatureRepository
{
    public EfSubCategoryFeatureRepository(ShopContext context) : base(context)
    {
    }
}
