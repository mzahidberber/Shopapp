using shopapp.core.Aspects.Logging;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
    [LogAspect(Priority = 1)]
    public class EfCategoryRepository : EfGenericRepository<Category>, ICategoryRepository
    {
        public EfCategoryRepository(ShopContext context) : base(context)
        {

        }



    }
}
