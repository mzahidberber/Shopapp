using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopapp.business.Concrete;

public class SubCategoryFeatureService : GenericService<SubCategoryFeature, SubCategoryFeatureDTO>, ISubCategoryFeatureService
{
    public SubCategoryFeatureService(ISubCategoryFeatureRepository genericRepository) : base(genericRepository)
    {
    }
}
