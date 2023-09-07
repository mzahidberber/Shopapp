using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.Business.Abstract;

public interface ISubCategoryFeatureValueService : IGenericService<SubCategoryFeatureValue, SubCategoryFeatureValueDTO>
{
    Task<Response<NoDataDTO>> SyncProductFeatures(int productId, int subCategoryId, List<string> features, List<string> values);
}
