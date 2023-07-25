using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
    public class EfCategoryRepository : EfGenericRepository<Category>,ICategoryRepository
    {
        public EfCategoryRepository(ShopContext context) : base(context)
        {
        }
    }
}
