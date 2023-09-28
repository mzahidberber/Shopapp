using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.core.DataAccess.Abstract;

public interface IOrderRepository : IGenericRepository<Order>
{
    IQueryable<Order> GetWhereWithProduct(Expression<Func<Order, bool>> filter);
    IQueryable<Order> GetAllWithUser(Expression<Func<Order, bool>>? filter = null);
    Task<Order> GetByIdWithUserAsync(int id);
}
