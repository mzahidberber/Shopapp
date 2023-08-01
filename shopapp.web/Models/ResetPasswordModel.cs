using System.ComponentModel.DataAnnotations;

namespace shopapp.web.Models
{
	public class ResetPasswordModel
    {
		public string Token { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
		public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
		public string Password { get; set; } = null!;
    }
}
