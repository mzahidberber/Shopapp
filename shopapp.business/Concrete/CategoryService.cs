using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete.Mapper;
using shopapp.core.Aspects.Logging;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.dataaccess.Concrete.EntityFramework;

namespace shopapp.business.Concrete;

[LogAspect(Priority = 1)]
public class CategoryService : GenericService<Category, CategoryDTO>, ICategoryService
{
    public ICategoryRepository _genericRepository { get; set; }

    public CategoryService(ICategoryRepository genericRepository) : base(genericRepository)
    {
        _genericRepository = genericRepository;    
    }

    public async Task<Response<CategoryDTO>> AddCheckUrlAndNameAsync(CategoryDTO entity)
    {
        var newEntity = ObjectMapper.Mapper.Map<Category>(entity);
        var result = await _genericRepository.GetAll().FirstOrDefaultAsync(x => x.Url == entity.Url || x.Name == entity.Name);
        if (result != null)
            return Response<CategoryDTO>.Fail("Url or name already exists ", 400, true);
        await _genericRepository.AddAsync(newEntity);
        await _genericRepository.CommitAsync();
        var newDto = ObjectMapper.Mapper.Map<CategoryDTO>(newEntity);
        return Response<CategoryDTO>.Success(newDto, 200);
    }

    public async Task<Response<NoDataDTO>> UpdateCheckUrlAndNameAsync(CategoryDTO entity, int id)
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
        entity.MainCategoryId = isExistEntity.MainCategoryId;
        var updateEntity = ObjectMapper.Mapper.Map<Category>(entity);
        _genericRepository.Update(updateEntity);
        await _genericRepository.CommitAsync();
        return Response<NoDataDTO>.Success(204);
    }

}
