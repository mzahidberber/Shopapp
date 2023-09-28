using shopapp.core.DTOs.Abstract;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.DTOs.Concrete
{
    public class OrderDTO : IDTO
    {
        public OrderDTO()
        {
            this.OrderItems = new List<OrderItemDTO>();
        }
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public EnumOrderState State { get; set; }
        public DateTime OrderTime { get; set; }
        public double TotalPrice { get; set; }
        public UserDTO User { get; set; }
        public string UserId { get; set; } = null!;
        public List<OrderItemDTO> OrderItems { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CityState { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }

        public string PaymentId { get; set; }
        public string ConversationId { get; set; }
        public EnumPaymentType PaymentType { get; set; }
    }
}
