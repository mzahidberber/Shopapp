using FluentValidation;
using shopapp.core.Entity.Concrete;
using shopapp.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopapp.core.Validation.web
{
    public class RegisterModelValidator: AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x=>x.Password)
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
