using System.ComponentModel.DataAnnotations;

namespace shopapp.web.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; } = null!;
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}
