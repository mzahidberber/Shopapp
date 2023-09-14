using Microsoft.EntityFrameworkCore;
using shopapp.core.Aspects.Logging;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Concrete;
using System.Linq;
using System.Linq.Expressions;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
    [LogAspect(Priority = 1)]
    public class EfProductRepository : EfGenericRepository<Product>, IProductRepository
    {
        public EfProductRepository(ShopContext context) : base(context)
        {
        }
        public IQueryable<Product> GetWhereWithAtt(Expression<Func<Product, bool>> filter)
        {
            return _dbSet.Where(filter).Include(x=>x.Brand).Include(x=>x.SubCategoryFeatureValues).ThenInclude(x=>x.SubCategoryFeature);
        }

        public IQueryable<Product> GetAllWithCategoriesAndBrands(Expression<Func<Product, bool>>? filter = null)
        {
            if (filter != null) return _dbSet.Where(filter)
                    .Include(x=>x.MainCategory)
                    .Include(x=>x.Category)
                    .Include(x=>x.SubCategory)
                    .Include(x=>x.Brand)
                    .AsQueryable();
            return _dbSet.Include(x => x.MainCategory)
                    .Include(x => x.Category)
                    .Include(x => x.SubCategory)
                    .Include(x => x.Brand).AsQueryable();
        }
        public IQueryable<Product> WherePage(int page, int pageSize,int sort, Expression<Func<Product, bool>>? filter)
        {

            if (filter == null)
                return _dbSet.Include(x => x.Brand).Skip((page - 1) * pageSize).Take(pageSize);
            else
                if(sort==2)
                    return _dbSet.Where(filter).Include(x=>x.Brand).OrderBy(x=>x.Price).Skip((page - 1) * pageSize).Take(pageSize);
                else if(sort==3)
                    return _dbSet.Where(filter).Include(x => x.Brand).OrderByDescending(x => x.Price).Skip((page - 1) * pageSize).Take(pageSize);
                else
                    return _dbSet.Where(filter).Include(x => x.Brand).Skip((page - 1) * pageSize).Take(pageSize);
        }
        public async Task<Product?> GetByIdWithCategoriesAsync(int id)
        {
            var entity = await _dbSet
                .Where(x => x.Id == id)
                .Include(x => x.Category)
                .SingleOrDefaultAsync();
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

		public async Task<Product?> GetByIdWithCategoriesAndImagesAsync(int id)
		{
			var entity = await _dbSet
				.Where(x => x.Id == id)
				.Include(x => x.Category)
                .Include(x=>x.Images)
				.SingleOrDefaultAsync();
			if (entity != null)
			{
				_context.Entry(entity).State = EntityState.Detached;
			}
			return entity;
		}

		public async Task<Product> GetByIdWithAttAsync(int id)
		{
			var entity = await _dbSet
				.Where(x => x.Id == id)
                .Include(x=>x.MainCategory)
				.Include(x => x.Category)
				.Include(x => x.SubCategory)
				.Include(x => x.Brand)
				.Include(x => x.SubCategoryFeatureValues)
				.ThenInclude(x => x.SubCategoryFeature)
				.Include(x => x.Images)
                //.Include(x=>x.Stocks)
                //.ThenInclude(x=>x.StockValues)
				.SingleOrDefaultAsync();
			if (entity != null)
			{
				_context.Entry(entity).State = EntityState.Detached;
			}
			return entity;
		}

		public async Task<Product> GetByUrlWithAttAsync(string url)
		{
			var entity = await _dbSet
				.Where(x => x.Url == url)
				.Include(x => x.MainCategory)
				.Include(x => x.Category)
				.Include(x => x.SubCategory)
				.Include(x => x.Brand)
				.Include(x => x.SubCategoryFeatureValues)
				.ThenInclude(x => x.SubCategoryFeature)
				.Include(x => x.Images)
				//.Include(x => x.Stocks)
				//.ThenInclude(x => x.StockValues)
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
                    .Include(x => x.Category)
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
