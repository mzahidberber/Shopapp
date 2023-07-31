using Microsoft.EntityFrameworkCore;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.Linq;
using System.Linq.Expressions;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
    public class EfCartItemRepository : EfGenericRepository<CartItem>,ICartItemRepository
    {
        public EfCartItemRepository(ShopContext context) : base(context)
        {
        }
    }
}
