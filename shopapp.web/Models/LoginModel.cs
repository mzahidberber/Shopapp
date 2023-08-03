using System.ComponentModel.DataAnnotations;

namespace shopapp.web.Models
{
	public class LoginModel
    {
        [Required(ErrorMessage ="Username is required.")]
		public string UserName { get; set; } = null!;
        //public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;
		public string? ReturnUrl { get; set; }
    }
}
