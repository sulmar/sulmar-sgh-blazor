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

    public async Task<(IEnumerable<Customer> Items, int TotalCount)> GetCustomersAsync(RequestParameters parameters)
    {
        var url = $"/api/customers?startIndex={parameters.StartIndex}&count={parameters.Count}";
        var response = await _client.GetAsync(url);

        var items = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
        var totalCount = int.Parse(response.Headers.GetValues("X-Total-Count").First());

        return (items, totalCount);


        //var customers = await _client.GetFromJsonAsync<IEnumerable<Customer>>();
    }
}
