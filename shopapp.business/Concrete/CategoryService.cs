using shopapp.core.Aspects.Logging;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.business.Concrete
{
    [LogAspect(Priority = 1)]
    public class CategoryService : GenericService<Category, CategoryDTO>, ICategoryService
    {
        public CategoryService(ICategoryRepository genericRepository) : base(genericRepository)
        {
        }
    }
}
