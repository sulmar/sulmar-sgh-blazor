using SGH.Shopper.Domain;

namespace SGH.Shopper.Api;

public static class CustomersEndpoints
{
    public static RouteGroupBuilder MapCustomers(this RouteGroupBuilder group)
    {
        group.MapGet(string.Empty, async (ICustomerRepository repository) => await repository.GetAllAsync());
        group.MapGet("{id:int}", async (int id, ICustomerRepository repository) => await repository.GetByIdAsync(id));
        group.MapPost(string.Empty, async (Customer customer, ICustomerRepository repository) => await repository.AddAsync(customer));

        return group;
    }
}
