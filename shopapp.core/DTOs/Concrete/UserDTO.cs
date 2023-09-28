namespace shopapp.core.DTOs.Concrete
{
    public class UserDTO
    {
        public UserDTO()
        {
            this.Orders = new List<OrderDTO>();
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
