using Microsoft.EntityFrameworkCore;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.dataaccess.Concrete.EntityFramework;

public class EfSubCategoryFeatureValueRepository : EfGenericRepository<SubCategoryFeatureValue>, ISubCategoryFeatureValueRepository
{
    public EfSubCategoryFeatureValueRepository(ShopContext context) : base(context)
    {
    }

    public IQueryable<SubCategoryFeatureValue> GetWhereWithFeature(Expression<Func<SubCategoryFeatureValue, bool>> filter)
    {
        return _dbSet.Where(filter).Include(x => x.SubCategoryFeature);
    }
}
