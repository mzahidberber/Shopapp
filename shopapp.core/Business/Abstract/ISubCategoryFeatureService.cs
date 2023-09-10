using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopapp.core.Business.Abstract;

public interface ISubCategoryFeatureService : IGenericService<SubCategoryFeature, SubCategoryFeatureDTO>
{
    Task<Response<List<SubCategoryFeatureDTO>>> AddManyAsync(List<SubCategoryFeatureDTO> entities);
    Task<Response<NoDataDTO>> SyncFeatures(int subCategoryId, List<string> features);
}
