using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.Business.Abstract
{
    public interface ICategoryService : IGenericService<Category, CategoryDTO>
    {
    }
}
