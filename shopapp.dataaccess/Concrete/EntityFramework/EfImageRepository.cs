using shopapp.core.Aspects.Logging;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework;

[LogAspect(Priority = 1)]
public class EfImageRepository : EfGenericRepository<Image>, IImageRepository
{
    public EfImageRepository(ShopContext context) : base(context)
    {
    }
}
