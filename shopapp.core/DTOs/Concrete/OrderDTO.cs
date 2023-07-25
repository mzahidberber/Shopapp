using shopapp.core.DTOs.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.DTOs.Concrete
{
    public class OrderDTO:IDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; }
    }
}
