using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete.Mapper;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.dataaccess.Concrete.EntityFramework;

namespace shopapp.business.Concrete;

public class SubCategoryService : GenericService<SubCategory, SubCategoryDTO>, ISubCategoryService
{
    public ISubCategoryRepository _genericRepository { get; set; }
    public SubCategoryService(ISubCategoryRepository genericRepository) : base(genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Response<SubCategoryDTO>> AddCheckUrlAndNameAsync(SubCategoryDTO entity)
    {
        var newEntity = ObjectMapper.Mapper.Map<SubCategory>(entity);
        var result = await _genericRepository.GetAll().FirstOrDefaultAsync(x => x.Url == entity.Url || x.Name == entity.Name);
        if (result != null)
            return Response<SubCategoryDTO>.Fail("Url or name already exists ", 400, true);
        await _genericRepository.AddAsync(newEntity);
        await _genericRepository.CommitAsync();
        var newDto = ObjectMapper.Mapper.Map<SubCategoryDTO>(newEntity);
        return Response<SubCategoryDTO>.Success(newDto, 200);
    }
}
