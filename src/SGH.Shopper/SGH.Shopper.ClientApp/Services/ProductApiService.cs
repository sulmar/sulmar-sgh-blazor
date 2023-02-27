using SGH.Shopper.Domain;
using System.Net.Http.Json;

namespace SGH.Shopper.ClientApp.Services;

public class ProductApiService
{
    private readonly HttpClient client;
    public ProductApiService(HttpClient client) => this.client = client;
    public async Task<IEnumerable<Product>> GetProductsAsync() => await client.GetFromJsonAsync<IEnumerable<Product>>("/api/products");
}