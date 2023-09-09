using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework;

public class EfSubCategoryRepository : EfGenericRepository<SubCategory>, ISubCategoryRepository
{
    public EfSubCategoryRepository(ShopContext context) : base(context)
    {
    }
}
