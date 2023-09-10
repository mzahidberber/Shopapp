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

    public async Task<Response<NoDataDTO>> UpdateCheckUrlAndNameAsync(SubCategoryDTO entity, int id)
    {
        var isExistEntity = await _genericRepository.GetByIdAsync(id);
        if (isExistEntity == null)
        {
            return Response<NoDataDTO>.Fail("Id Not Found", 404, true);

        }
        var categories = _genericRepository.GetAll();
        if (isExistEntity.Url != entity.Url)
        {
            var result = await categories.FirstOrDefaultAsync(x => x.Url == entity.Url);
            if (result != null)
                return Response<NoDataDTO>.Fail("Url or name already exists ", 400, true);
        }
        if (isExistEntity.Name != entity.Name)
        {
            var result = await categories.FirstOrDefaultAsync(x => x.Name == entity.Name);
            if (result != null)
                return Response<NoDataDTO>.Fail("Url or name already exists ", 400, true);
        }

        var updateEntity = ObjectMapper.Mapper.Map<SubCategory>(entity);
        _genericRepository.Update(updateEntity);
        await _genericRepository.CommitAsync();
        return Response<NoDataDTO>.Success(204);
    }
}
