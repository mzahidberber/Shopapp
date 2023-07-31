using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.business.Concrete
{
    public class CartItemService : GenericService<CartItem, CartItemDTO>, ICartItemService
    {
        public CartItemService(ICartItemRepository genericRepository) : base(genericRepository)
        {
        }
    }
}
