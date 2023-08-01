using System.ComponentModel.DataAnnotations;

namespace shopapp.web.Models
{
	public class LoginModel
    {
		public string UserName { get; set; } = null!;
        //public string? Email { get; set; }

        [DataType(DataType.Password)]
		public string Password { get; set; } = null!;
		public string? ReturnUrl { get; set; }
    }
}
