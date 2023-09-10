using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.core.Business.Abstract;

public interface ISubCategoryFeatureValueService : IGenericService<SubCategoryFeatureValue, SubCategoryFeatureValueDTO>
{
    Task<Response<NoDataDTO>> SyncProductFeatures(int productId, int subCategoryId, List<string> features, List<string> values);
    Task<Response<IEnumerable<SubCategoryFeatureValueDTO>>> WhereWithFeaturesAsync(Expression<Func<SubCategoryFeatureValue, bool>> filter);
}
