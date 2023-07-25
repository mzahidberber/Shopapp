using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.business.Concrete
{
    public class CategoryService : GenericService<Category, CategoryDTO>,ICategoryService
    {
        public CategoryService(ICategoryRepository genericRepository) : base(genericRepository)
        {
        }
    }
}
