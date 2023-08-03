using System.ComponentModel.DataAnnotations;

namespace shopapp.web.Models
{
	public class ResetPasswordModel
    {
		public string Token { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Email is not correct.")]
		public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?.!@$%^&*-]).{8,}$", ErrorMessage = "Password must be least 8 digits and contain alphanumeric,uppercase,lowercase letters")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "RePassword is required.")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; } = null!;
    }
}
