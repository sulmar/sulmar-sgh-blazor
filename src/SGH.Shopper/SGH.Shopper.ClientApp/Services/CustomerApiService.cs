using SGH.Shopper.Domain;
using System.Net.Http.Json;

namespace SGH.Shopper.ClientApp.Services;

public class CustomerApiService
{
    private readonly HttpClient _client;

    public CustomerApiService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync() => await _client.GetFromJsonAsync<IEnumerable<Customer>>("/api/customers");
}
