using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Concrete;
using System.Linq.Expressions;

namespace shopapp.core.Business.Abstract;


public interface IOrderService : IGenericService<Order, OrderDTO>
{
    Task<Response<IEnumerable<OrderDTO>>> WhereWithProducts(Expression<Func<Order, bool>> predicate);
    Task<Response<IEnumerable<OrderDTO>>> GetAllWithUserAsync();
    Task<Response<OrderDTO>> GetByIdWithUserAsync(int id);
    Task<Response<OrderDTO>> ChangeOrderState(int id, EnumOrderState state);
}
