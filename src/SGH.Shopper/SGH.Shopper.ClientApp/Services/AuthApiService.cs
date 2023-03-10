using SGH.Shopper.ClientApp.Models;
using System.Net.Http.Json;

namespace SGH.Shopper.ClientApp.Services;

public class AuthApiService
{
    private readonly HttpClient client;

    public AuthApiService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<string> CreateTokenAsync(LoginRequest request)
    {
        var response = await client.PostAsJsonAsync("api/token/create", request);

        var token = await response.Content.ReadFromJsonAsync<string>();

        return token;
    }
}

