using shopapp.core.Entity.Concrete;

namespace shopapp.core.DataAccess.Abstract
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        public Task<Cart?> GetCartByUserId(string id);
    }
}
