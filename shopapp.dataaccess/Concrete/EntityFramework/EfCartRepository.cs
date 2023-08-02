using Microsoft.EntityFrameworkCore;
using shopapp.core.Aspects.Logging;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.Linq;
using System.Linq.Expressions;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
	[LogAspect(Priority = 1)]
	public class EfCartRepository : EfGenericRepository<Cart>,ICartRepository
    {
        public EfCartRepository(ShopContext context) : base(context)
        {
        }

		public async Task<Cart?> GetCartByUserId(string id)
		{
			return await _dbSet
				.Where(x => x.UserId == id)
				.Include(x => x.CartItems)
				.ThenInclude(x => x.Product)
				.FirstOrDefaultAsync(x=>x.UserId==id);
		}
	}
}
