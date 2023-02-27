using SGH.Shopper.Domain;
using System.Net.Http.Json;

namespace SGH.Shopper.ClientApp.Services;

public class NasaService
{
    private readonly HttpClient client;

    public NasaService(HttpClient client) => this.client = client;

    public async Task<IEnumerable<Planet>> GetPlanets (int count = 10) => await client.GetFromJsonAsync<IEnumerable<Planet>>($"/planetary/apod?api_key=DEMO_KEY&count={count}");
}