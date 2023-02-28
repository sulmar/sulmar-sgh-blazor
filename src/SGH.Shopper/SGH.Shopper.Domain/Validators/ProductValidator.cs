using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGH.Shopper.Domain.Validators;

// dotnet add package FluentValidation

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(3).MaximumLength(20);
        RuleFor(p => p.Barcode).Length(14);
        RuleFor(p => p.Price).InclusiveBetween(1, 1000);
        RuleFor(p => p.PriceDiscount).InclusiveBetween(1, 1000).When(p => p.HasDiscount);
    }
}
