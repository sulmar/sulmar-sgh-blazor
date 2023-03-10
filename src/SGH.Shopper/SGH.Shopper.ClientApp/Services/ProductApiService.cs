using Microsoft.Net.Http.Headers;
using SGH.Shopper.Domain;
using System.Net.Http.Json;

namespace SGH.Shopper.ClientApp.Services;

public class CustomAuthorizationMessageHandler : DelegatingHandler
{
    private readonly IStorageProvider storageProvider;

    public CustomAuthorizationMessageHandler(IStorageProvider storageProvider)
    {
        this.storageProvider = storageProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {        
        if (!request.Headers.Contains("Authorization"))
        {
            var token = storageProvider.GetItem("token");

            if (token != null)
            {
                request.Headers.Add(HeaderNames.Authorization, $"Bearer {token}");
                
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }

}

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

        if (response.IsSuccessStatusCode)
        {
            var items = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
            var totalCount = int.Parse(response.Headers.GetValues("X-Total-Count").First());

            return new PagingResponse<Product> { Items = items, TotalCount = totalCount };
        }

        return new PagingResponse<Product>();
    }

    public async Task<Product> GetProductById(int id) => await client.GetFromJsonAsync<Product>($"/api/products/{id}");
    public async Task UpdateProduct(Product product) => await client.PutAsJsonAsync($"/api/products/{product.Id}", product);
}