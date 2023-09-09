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

}
