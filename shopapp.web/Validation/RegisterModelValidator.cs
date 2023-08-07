using FluentValidation;
using shopapp.web.Models.Account;

namespace shopapp.core.Validation.web
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("MyProperty boş olamaz.")
                .Must(BeAlphanumeric).WithMessage("MyProperty sadece alfa numerik karakterler içermelidir.");
        }

        private bool BeAlphanumeric(string value)
        {
            // Burada alphanumeric kontrolü yapılacak.
            return !string.IsNullOrEmpty(value) && value.All(char.IsLetterOrDigit);
        }
    }
}
