using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete.Mapper;
using shopapp.core.Business.Abstract;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using shopapp.dataaccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace shopapp.business.Concrete;

public class OrderService : GenericService<Order, OrderDTO>, IOrderService
{
    public IOrderRepository _genericRepository { get; set; }
    public OrderService(IOrderRepository genericRepository) : base(genericRepository)
	{
		_genericRepository = genericRepository;
	}

    public async Task<Response<OrderDTO>> ChangeOrderState(int id,EnumOrderState state)
    {
        var product = await _genericRepository.GetByIdAsync(id);
        if (product == null)
        {
            return Response<OrderDTO>.Fail("Id Not Found", 404, true);
        }
        if ((int)product.State<6)
            product.State=state; 
        _genericRepository.Update(product);
        await _genericRepository.CommitAsync();
        
        return Response<OrderDTO>.Success(ObjectMapper.Mapper.Map<OrderDTO>(product), 200);
    }

    public async Task<Response<OrderDTO>> GetByIdWithUserAsync(int id)
    {
        var product = await _genericRepository.GetByIdWithUserAsync(id);
        await _genericRepository.CommitAsync();
        if (product == null)
        {
            return Response<OrderDTO>.Fail("Id Not Found", 404, true);
        }
        return Response<OrderDTO>.Success(ObjectMapper.Mapper.Map<OrderDTO>(product), 200);
    }

    public async Task<Response<IEnumerable<OrderDTO>>> GetAllWithUserAsync()
    {
        var products = ObjectMapper.Mapper.Map<List<OrderDTO>>(await _genericRepository.GetAllWithUser().ToListAsync());
        await _genericRepository.CommitAsync();
        return Response<IEnumerable<OrderDTO>>.Success(products, 200);
    }

    public async Task<Response<IEnumerable<OrderDTO>>> WhereWithProducts(Expression<Func<Order, bool>> predicate)
	{
		var list = await _genericRepository.GetWhereWithProduct(predicate).ToListAsync();
		await _genericRepository.CommitAsync();
		return Response<IEnumerable<OrderDTO>>.Success(ObjectMapper.Mapper.Map<IEnumerable<OrderDTO>>(list), 200);
	}
}
