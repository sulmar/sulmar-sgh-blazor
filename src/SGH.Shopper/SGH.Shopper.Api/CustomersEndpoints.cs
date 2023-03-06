using SGH.Shopper.Domain;

namespace SGH.Shopper.Api;

public static class CustomersEndpoints
{
    public static RouteGroupBuilder MapCustomers(this RouteGroupBuilder group)
    {

        // group.MapGet(string.Empty, async (ICustomerRepository repository) => await repository.GetAllAsync());

        // GET /api/customers?startIndex={startIndex}&count={count}
        group.MapGet(string.Empty, async ([AsParameters]RequestParameters parameters, 
            ICustomerRepository repository,
            HttpResponse httpResponse
            ) =>
        {
            var response = await repository.GetAllAsync(parameters);

            httpResponse.Headers.Add("X-Total-Count", response.TotalCount.ToString());

            return response.Items;

        });

        group.MapGet("{id:int}", async (int id, ICustomerRepository repository) => await repository.GetByIdAsync(id));
        group.MapPost(string.Empty, async (Customer customer, ICustomerRepository repository) => await repository.AddAsync(customer));

        

        return group;
    }
}
