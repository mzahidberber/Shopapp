using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.Business.Abstract
{
    public interface ICartService:IGenericService<Cart,CartDTO>
    {
        Task<Response<CartDTO>> GetCartByUserId(string userId);
    }
}
