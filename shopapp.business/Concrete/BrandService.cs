using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete.Mapper;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.business.Concrete;

public class BrandService : GenericService<Brand, BrandDTO>, IBrandService
{
    public IBrandRepository _genericRepository { get; set; }
    public BrandService(IBrandRepository genericRepository) : base(genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Response<BrandDTO>> AddCheckUrlAndNameAsync(BrandDTO entity)
    {
        var newEntity = ObjectMapper.Mapper.Map<Brand>(entity);
        var result = await _genericRepository.GetAll().FirstOrDefaultAsync(x => x.Url == entity.Url || x.Name == entity.Name);
        if (result != null)
            return Response<BrandDTO>.Fail("Url or name already exists ", 400, true);
        await _genericRepository.AddAsync(newEntity);
        await _genericRepository.CommitAsync();
        var newDto = ObjectMapper.Mapper.Map<BrandDTO>(newEntity);
        return Response<BrandDTO>.Success(newDto, 200);
    }

    public async Task<Response<BrandProductCount>> GetProductCountAsync(int id)
    {
        var count = _genericRepository.GetWhere(x => x.Id == id).SelectMany(x => x.Products).Count();
        await _genericRepository.CommitAsync();
        return Response<BrandProductCount>.Success(new BrandProductCount
        {
            ProductCount = count
        }, 200);
    }
    public async Task<Response<NoDataDTO>> UpdateCheckUrlAndNameAsync(BrandDTO entity, int id)
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

        var updateEntity = ObjectMapper.Mapper.Map<Brand>(entity);
        _genericRepository.Update(updateEntity);
        await _genericRepository.CommitAsync();
        return Response<NoDataDTO>.Success(204);
    }

    public async Task<Response<NoDataDTO>> UpdateLastSubCategory(BrandDTO entity, int id)
    {
        var isExistEntity = await _genericRepository.GetByIdAsync(id);
        if (isExistEntity == null)
        {
            return Response<NoDataDTO>.Fail("Id Not Found", 404, true);

        }
        entity.SubCategoryId = isExistEntity.SubCategoryId;
        var updateEntity = ObjectMapper.Mapper.Map<Brand>(entity);
        _genericRepository.Update(updateEntity);
        await _genericRepository.CommitAsync();
        return Response<NoDataDTO>.Success(204);
    }
}
