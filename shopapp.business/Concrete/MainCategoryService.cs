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


        public async Task<Response<IEnumerable<MainCategoryDTO>>> GetAllWithCategoriAndSubCategories()
        {
            var mainCategories=await this._genericRepository.GetAllWithCategoriesAndSubCategories().ToListAsync();
            
            return Response<IEnumerable<MainCategoryDTO>>.Success(ObjectMapper.Mapper.Map<IEnumerable<MainCategoryDTO>>(mainCategories),200);
        }

        public async Task<Response<IEnumerable<MainCategoryDTO>>> GetAllWithCategoriAndSubCategoriesAndBrands()
        {
            var mainCategories = await this._genericRepository.GetAllWithCategoriesAndSubCategoriesAndBrands().ToListAsync();

            return Response<IEnumerable<MainCategoryDTO>>.Success(ObjectMapper.Mapper.Map<IEnumerable<MainCategoryDTO>>(mainCategories), 200);
        }
    }
}
