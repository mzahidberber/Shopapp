using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopapp.dataaccess.Concrete.EntityFramework;

public class EfSubCategoryFeatureRepository : EfGenericRepository<SubCategoryFeature>, ISubCategoryFeatureRepository
{
    public EfSubCategoryFeatureRepository(ShopContext context) : base(context)
    {
    }
}
