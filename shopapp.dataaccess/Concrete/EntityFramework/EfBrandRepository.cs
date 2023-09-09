using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework;

public class EfBrandRepository : EfGenericRepository<Brand>, IBrandRepository
{
    public EfBrandRepository(ShopContext context) : base(context)
    {
    }
}
