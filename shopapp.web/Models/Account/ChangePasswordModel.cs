using System.ComponentModel.DataAnnotations;

namespace shopapp.web.Models.Account
{
	public class ChangePasswordModel
	{
		[Required(ErrorMessage = "Username is required.")]
		public string userName { get; set; } = null!;

		[Required(ErrorMessage = "OldPassword is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?.!@$%^&*-]).{8,}$", ErrorMessage = "OldPassword must be least 8 digits and contain alphanumeric,uppercase,lowercase letters")]
        public string OldPassword { get; set; } = null!;

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
