using shopapp.core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace shopapp.core.DataAccess.Abstract;

public interface ISubCategoryFeatureValueRepository : IGenericRepository<SubCategoryFeatureValue>
{
    IQueryable<SubCategoryFeatureValue> GetWhereWithFeature(Expression<Func<SubCategoryFeatureValue, bool>> filter);
}
