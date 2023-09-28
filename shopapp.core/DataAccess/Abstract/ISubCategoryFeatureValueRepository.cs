using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.core.DataAccess.Abstract;

public interface ISubCategoryFeatureValueRepository : IGenericRepository<SubCategoryFeatureValue>
{
    IQueryable<SubCategoryFeatureValue> GetWhereWithFeature(Expression<Func<SubCategoryFeatureValue, bool>> filter);
}
