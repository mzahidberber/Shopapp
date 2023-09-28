using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.Business.Abstract;

public interface ISubCategoryFeatureService : IGenericService<SubCategoryFeature, SubCategoryFeatureDTO>
{
    Task<Response<List<SubCategoryFeatureDTO>>> AddManyAsync(List<SubCategoryFeatureDTO> entities);
    Task<Response<NoDataDTO>> SyncFeatures(int subCategoryId, List<string> features);
}
