using Bogus;
using SGH.Shopper.Domain;

namespace SGH.Shopper.Infrastructure.Fakers;

public class CustomerFaker : Faker<Customer>
{
    public CustomerFaker()
    {
        RuleFor(p => p.Id, f => f.IndexFaker);
        RuleFor(p => p.FirstName, f => f.Person.FirstName);
        RuleFor(p=>p.LastName, f  => f.Person.LastName);
    }
}
