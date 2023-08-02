using Microsoft.EntityFrameworkCore;
using shopapp.core.Aspects.Caching;
using shopapp.core.Aspects.Logging;
using shopapp.core.CrossCuttingConcers.Caching.Microsoft;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
    [LogAspect(Priority = 1)]
    public class EfProductRepository : EfGenericRepository<Product>,IProductRepository
    {
        public EfProductRepository(ShopContext context) : base(context)
        {
        }
        public IQueryable<Product> WherePage(int page,int pageSize, Expression<Func<Product, bool>>? filter)
        {

            if(filter == null)
                return _dbSet.Skip((page - 1) * pageSize).Take(pageSize);
            else
                return _dbSet.Where(filter).Skip((page-1)*pageSize).Take(pageSize);
        }
        public async Task<Product> GetByIdWithCategoriesAsync(int id)
        {
            var entity = await _dbSet
                .Where(x=>x.Id==id)
                .Include(x=>x.ProductCategories)
                .ThenInclude(x=>x.Category)
                .SingleOrDefaultAsync();
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public IQueryable<Product> WhereWithCategories(Expression<Func<Product, bool>> filter)
        {
            return _dbSet.Where(filter)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Category)
                    .AsQueryable();
        }

        public async Task<int> GetCountByCategory(Expression<Func<Product, bool>>? filter = null)
        {
            if (filter == null)
                return await _dbSet.CountAsync();
            else
                return await _dbSet.Where(filter).CountAsync();
        }
    }
}
