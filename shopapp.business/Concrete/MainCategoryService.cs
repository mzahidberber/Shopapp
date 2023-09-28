using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete.Mapper;
using shopapp.core.Aspects.Logging;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.business.Concrete
{
    [LogAspect(Priority = 1)]
    public class MainCategoryService : GenericService<MainCategory, MainCategoryDTO>, IMainCategoryService
    {
        private readonly IMainCategoryRepository _genericRepository;
        public MainCategoryService(IMainCategoryRepository genericRepository) : base(genericRepository)
        {
            this._genericRepository = genericRepository;
        }

        public async Task<Response<MainCategoryAndProductCountDTO>> GetByIdWithProductCountAndCategories(int id)
        {
            var maincategory = _genericRepository.GetWhere(x => x.Id == id);
            var count = maincategory.SelectMany(x => x.Products).Count();
            var categories = await _genericRepository.GetAllWithCategoriesAndSubCategories().Where(x => x.Id == id).ToListAsync();
            await _genericRepository.CommitAsync();
            return Response<MainCategoryAndProductCountDTO>.Success(new MainCategoryAndProductCountDTO
            {
                ProductCount = count,
                Categories = ObjectMapper.Mapper.Map<List<CategoryDTO>>(categories.SelectMany(x => x.Categories))
            }, 200);
        }

        public async Task<Response<IEnumerable<MainCategoryDTO>>> GetAllWithCategoriAndSubCategories()
        {
            var mainCategories = await this._genericRepository.GetAllWithCategoriesAndSubCategories().ToListAsync();
            await _genericRepository.CommitAsync();
            return Response<IEnumerable<MainCategoryDTO>>.Success(ObjectMapper.Mapper.Map<IEnumerable<MainCategoryDTO>>(mainCategories), 200);
        }

        public async Task<Response<IEnumerable<MainCategoryDTO>>> GetAllWithCategoriAndSubCategoriesAndBrands()
        {
            var mainCategories = await this._genericRepository.GetAllWithCategoriesAndSubCategoriesAndBrands().ToListAsync();
            await _genericRepository.CommitAsync();
            return Response<IEnumerable<MainCategoryDTO>>.Success(ObjectMapper.Mapper.Map<IEnumerable<MainCategoryDTO>>(mainCategories), 200);
        }

        public async Task<Response<MainCategoryDTO>> AddCheckUrlAndNameAsync(MainCategoryDTO entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<MainCategory>(entity);
            var result = await _genericRepository.GetAll().FirstOrDefaultAsync(x => x.Url == entity.Url || x.Name == entity.Name);
            if (result != null)
                return Response<MainCategoryDTO>.Fail("Url or name already exists ", 400, true);
            await _genericRepository.AddAsync(newEntity);
            await _genericRepository.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<MainCategoryDTO>(newEntity);
            return Response<MainCategoryDTO>.Success(newDto, 200);
        }
        public async Task<Response<NoDataDTO>> UpdateCheckUrlAndNameAsync(MainCategoryDTO entity, int id)
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

            var updateEntity = ObjectMapper.Mapper.Map<MainCategory>(entity);
            _genericRepository.Update(updateEntity);
            await _genericRepository.CommitAsync();
            return Response<NoDataDTO>.Success(204);
        }

    }
}
