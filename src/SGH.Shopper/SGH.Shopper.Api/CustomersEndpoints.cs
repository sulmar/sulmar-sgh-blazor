using SGH.Shopper.Domain;

namespace SGH.Shopper.Api;

public static class CustomersEndpoints
{
    public static RouteGroupBuilder MapCustomers(this RouteGroupBuilder group)
    {
        group.MapGet(string.Empty, () => Results.Ok());
        group.MapGet("{id:int}", (int id) => Results.Ok());
        group.MapPost(string.Empty, (Customer customer) => Results.Ok());

        return group;
    }
}
