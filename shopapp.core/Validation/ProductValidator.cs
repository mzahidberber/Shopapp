using FluentValidation;
using shopapp.core.Entity.Concrete;

namespace shopapp.core.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {

        }
    }
}
