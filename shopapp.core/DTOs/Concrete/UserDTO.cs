using shopapp.core.DTOs.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.DTOs.Concrete
{
    public class UserDTO : IDTO
    {
        public UserDTO()
        {
            this.Orders = new List<Order>();
        }
        public int Id { get; set; }
        public List<Order> Orders { get; set; }
    }
}
