using Microsoft.EntityFrameworkCore;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.Linq;
using System.Linq.Expressions;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
    public class EfProductRepository : EfGenericRepository<Product>,IProductRepository
    {
        public EfProductRepository(ShopContext context) : base(context)
        {
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
    }
}
