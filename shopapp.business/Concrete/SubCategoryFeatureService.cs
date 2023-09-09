using shopapp.business.Concrete.Mapper;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.dataaccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        return Response< List < SubCategoryFeatureDTO >>.Success(newDtos, 200);
    }
}
