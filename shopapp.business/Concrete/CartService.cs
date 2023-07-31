using shopapp.business.Concrete.Mapper;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;

namespace shopapp.business.Concrete
{
    public class CartService : GenericService<Cart, CartDTO>, ICartService
    {
		private readonly ICartRepository _genericRepository;
        public CartService(ICartRepository genericRepository):base(genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Response<CartDTO>> GetCartByUserId(string userId)
		{
			var cart = await _genericRepository.GetCartByUserId(userId);
			await _genericRepository.CommitAsync();
			if (cart == null)
			{
				return Response<CartDTO>.Fail("Id Not Found", 404, true);
			}
			return Response<CartDTO>.Success(ObjectMapper.Mapper.Map<CartDTO>(cart), 200);
		}
	}
}
