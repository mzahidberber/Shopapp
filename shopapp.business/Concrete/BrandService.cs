﻿using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete.Mapper;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.dataaccess.Concrete.EntityFramework;

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
}