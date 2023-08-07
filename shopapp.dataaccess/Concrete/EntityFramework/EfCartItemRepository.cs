using shopapp.core.Aspects.Logging;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
    [LogAspect(Priority = 1)]
    public class EfCartItemRepository : EfGenericRepository<CartItem>, ICartItemRepository
    {
        public EfCartItemRepository(ShopContext context) : base(context)
        {
        }
    }
}
