using shopapp.core.Entity.Abstract;

namespace shopapp.core.Entity.Concrete
{
    public class Order : IEntity
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public EnumOrderState State { get; set; }
        public DateTime OrderTime { get; set; }
        public double TotalPrice { get; set; }
        public User User { get; set; }
        public string UserId { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CityState { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? Note { get; set; }

        public string PaymentId { get; set; }
        public string ConversationId { get; set; }
        public EnumPaymentType PaymentType { get; set; }
    }

    public enum EnumPaymentType
    {
        CreditCard = 0, Eft = 1
    }

    public enum EnumOrderState
    {
        HasbeenTaken = 0, Waiting = 1, Unpaid = 2, GettingReady = 3, InCargo = 4, HasDelivered = 5
    }
}
