using shopapp.business.Concrete.Mapper;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.business.Concrete;

public class SubCategoryFeatureService : GenericService<SubCategoryFeature, SubCategoryFeatureDTO>, ISubCategoryFeatureService
{
    public ISubCategoryFeatureRepository _genericRepository { get; set; }
    public SubCategoryFeatureService(ISubCategoryFeatureRepository genericRepository) : base(genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Response<List<SubCategoryFeatureDTO>>> AddManyAsync(List<SubCategoryFeatureDTO> entities)
    {
        var newEntities = ObjectMapper.Mapper.Map<List<SubCategoryFeature>>(entities);
        foreach (var e in newEntities)
        {
            await _genericRepository.AddAsync(e);
        }

        await _genericRepository.CommitAsync();
        var newDtos = ObjectMapper.Mapper.Map<List<SubCategoryFeatureDTO>>(newEntities);
        return Response<List<SubCategoryFeatureDTO>>.Success(newDtos, 200);
    }

    public async Task<Response<NoDataDTO>> SyncFeatures(int subCategoryId, List<string> features)
    {
        var featureModels = _genericRepository.GetWhere(x => x.SubCategoryId == subCategoryId).ToList();
        foreach (var f in featureModels)
        {
            if (!features.Any(x => x == f.Name))
            {
                _genericRepository.Delete(f);
            }
        }
        foreach (var f in features)
        {
            if (!featureModels.Any(x => x.Name == f))
            {
                await _genericRepository.AddAsync(new SubCategoryFeature
                {
                    Name = f,
                    SubCategoryId = subCategoryId
                });
            }
        }

        await _genericRepository.CommitAsync();
        return Response<NoDataDTO>.Success(204);

    }
}
