using Bogus;
using SGH.Shopper.Domain;

namespace SGH.Shopper.Infrastructure.Fakers;

// dotnet add package Bogus
public class ProductFaker : Faker<Product>
{
    public ProductFaker()
    {
        UseSeed(1);
        StrictMode(true);
        RuleFor(p => p.Id, f => f.IndexFaker);
        RuleFor(p => p.Name, f => f.Commerce.ProductName());
        RuleFor(p => p.Description, f => f.Commerce.ProductDescription());
        RuleFor(p => p.Barcode, f => f.Commerce.Ean13());
        RuleFor(p => p.Color, f => f.Commerce.Color());
        RuleFor(p => p.Price, f => Math.Round(f.Random.Decimal(1, 100), 2));
        RuleFor(p => p.HasDiscount, f => f.Random.Bool(0.2f));
        RuleFor(p => p.PriceDiscount, (f, product) => product.HasDiscount ? Math.Round(f.Random.Decimal(1, product.Price), 2) : 0);
        RuleFor(p => p.Size, f => f.PickRandom<Size>().OrNull(f, 0.4f));
    }
}
