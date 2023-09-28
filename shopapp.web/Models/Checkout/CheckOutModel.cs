using System.ComponentModel.DataAnnotations;

namespace shopapp.web.Models.Checkout;

public class CheckOutModel
{
    public CheckOutModel()
    {
        this.Products = new List<CheckoutProduct>();
    }

    [Display(Name = "FirstName", Prompt = "Enter first name")]
    [Required(ErrorMessage = "First name required!")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "First name 2 -100 char")]
    public string FirstName { get; set; }

    [Display(Name = "LastName", Prompt = "Enter last name")]
    [Required(ErrorMessage = "Last name required!")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name 2 -100 char")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email required!")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email is not correct.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number required!")]
    [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is not correct.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Address required!")]
    public string Adress { get; set; }

    [Required(ErrorMessage = "City required!")]
    public string City { get; set; }

    [Required(ErrorMessage = "State required!")]
    public string CityState { get; set; }
    public string? Note { get; set; }

    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? ExpirationMonth { get; set; }
    public string? ExpirationYear { get; set; }
    public string? CVV { get; set; }

    public List<CheckoutProduct> Products { get; set; }

    public double TotalPrice()
    {
        return Products.Sum(i => (i.Price) * i.Quantity);
    }

}
