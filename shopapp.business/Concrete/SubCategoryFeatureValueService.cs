using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete.Mapper;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.business.Concrete;

public class SubCategoryFeatureValueService : GenericService<SubCategoryFeatureValue, SubCategoryFeatureValueDTO>, ISubCategoryFeatureValueService
{
    public ISubCategoryFeatureValueRepository _repository { get; set; }
    public ISubCategoryFeatureRepository _repositoryFeature { get; set; }
    public SubCategoryFeatureValueService(ISubCategoryFeatureValueRepository genericRepository, ISubCategoryFeatureRepository repositoryFeature) : base(genericRepository)
    {
        _repository = genericRepository;
        _repositoryFeature = repositoryFeature;
    }

    public async Task<Response<IEnumerable<SubCategoryFeatureValueDTO>>> WhereWithFeaturesAsync(Expression<Func<SubCategoryFeatureValue, bool>> filter)
    {
        var features = ObjectMapper.Mapper.Map<IEnumerable<SubCategoryFeatureValueDTO>>(_repository.GetWhereWithFeature(filter).AsEnumerable());
        await _repository.CommitAsync();
        return Response<IEnumerable<SubCategoryFeatureValueDTO>>.Success(features,200);
    }

    public async  Task<Response<NoDataDTO>> SyncProductFeatures(int productId,int subCategoryId,List<string> features, List<string> values)
    {
        var featureModels=_repository.GetWhereWithFeature(x => x.ProductId == productId).ToList();
        if(featureModels.First().SubCategoryFeature.SubCategoryId == subCategoryId) {
            for (int i = 0; i < featureModels.Count(); i++)
            {
                featureModels[i].Value = values[features.IndexOf(features.First(x => x == featureModels[i].SubCategoryFeature.Name))];
            }
        }
        else
        {
            foreach (var f in featureModels)
            {
                _repository.Delete(f);
            }
            for (int i = 0; i < features.Count; i++)
            {
                await _repository.AddAsync(new SubCategoryFeatureValue
                {
                    Value = values[i],
                    ProductId= productId,
                    SubCategoryFeatureId = _repositoryFeature.GetWhere(x => x.Name == features[i]).First().Id
                });
            }
        }

        await _repository.CommitAsync();
        return Response<NoDataDTO>.Success(204);

    }
}
