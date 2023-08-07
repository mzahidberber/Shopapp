using System.ComponentModel.DataAnnotations;

namespace shopapp.web.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?.!@$%^&*-]).{8,}$", ErrorMessage = "Password must be least 8 digits and contain alphanumeric,uppercase,lowercase letters")]
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
    }
}
