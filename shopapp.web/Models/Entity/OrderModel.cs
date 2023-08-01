using shopapp.core.Entity.Concrete;

namespace shopapp.web.Models.Entity
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public UserModel User { get; set; }
	}
}
