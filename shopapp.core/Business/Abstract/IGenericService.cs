using shopapp.core.DTOs.Concrete;
using System.Linq.Expressions;

namespace shopapp.core.Business.Abstract
{
    public interface IGenericService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<Response<TDto>> AddAsync(TDto entity);
        Task<Response<NoDataDTO>> Remove(int id);
        Task<Response<NoDataDTO>> Update(TDto entity, int id);
    }
}
