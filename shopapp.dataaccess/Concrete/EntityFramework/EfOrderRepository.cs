using Microsoft.EntityFrameworkCore;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.dataaccess.Concrete.EntityFramework;

public class EfOrderRepository : EfGenericRepository<Order>, IOrderRepository
{
    public EfOrderRepository(ShopContext context) : base(context)
    {
    }
    public async Task<Order> GetByIdWithUserAsync(int id)
    {
        var entity = await _dbSet.Where(x => x.Id == id)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .Include(x => x.User).FirstOrDefaultAsync();
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        return entity;
    }
    public IQueryable<Order> GetAllWithUser(Expression<Func<Order, bool>>? filter = null)
    {
        if (filter != null) return _dbSet.Where(filter).Include(x => x.User).AsQueryable();
        return _dbSet.Include(x => x.User).AsQueryable();
    }

    public IQueryable<Order> GetWhereWithProduct(Expression<Func<Order, bool>> filter)
    {
        return _dbSet.Where(filter)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Brand)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.SubCategory);
    }
}
