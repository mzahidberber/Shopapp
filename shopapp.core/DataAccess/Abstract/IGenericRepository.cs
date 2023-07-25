using shopapp.core.Entity.Abstract;
using System.Linq.Expressions;

namespace shopapp.core.DataAccess.Abstract
{
    public interface IGenericRepository<T>:IUnitOfWork
        where T : class,IEntity,new()
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
