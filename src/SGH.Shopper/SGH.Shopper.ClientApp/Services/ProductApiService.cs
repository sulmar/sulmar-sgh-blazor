using SGH.Shopper.Domain;
using System.Net.Http.Json;

namespace SGH.Shopper.ClientApp.Services;

public class ProductApiService
{
    private readonly HttpClient client;
    public ProductApiService(HttpClient client) => this.client = client;
    public async Task<IEnumerable<Product>> GetProductsAsync() => await client.GetFromJsonAsync<IEnumerable<Product>>("/api/products");
    public async Task<IEnumerable<Product>> GetProductsByContentAsync(string content) =>
        await client.GetFromJsonAsync<IEnumerable<Product>>($"/api/products/search?filter={content}");

    public async Task<PagingResponse<Product>> GetProductsAsync(PagingParameters parameters)
    {
        var url = $"/api/products/paging?pageSize={parameters.PageSize}&pageNumber={parameters.PageNumber}";

        // var response = await client.GetFromJsonAsync<IEnumerable<Product>>(url);

        var response = await client.GetAsync(url);

        var items = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        var totalCount = int.Parse(response.Headers.GetValues("X-Total-Count").First());

        return new PagingResponse<Product> { Items = items, TotalCount = totalCount };
    }
}