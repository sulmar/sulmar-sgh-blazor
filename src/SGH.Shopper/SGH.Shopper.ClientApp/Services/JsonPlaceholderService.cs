using SGH.Shopper.Domain;
using System.Net.Http.Json;

namespace SGH.Shopper.ClientApp.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUserById(int id);
}

public class JsonPlaceholderService : IUserService
{
    private readonly HttpClient client;
    public JsonPlaceholderService(HttpClient client) => this.client = client;
    public async Task<IEnumerable<User>> GetUsers() => await client.GetFromJsonAsync<IEnumerable<User>>("users");
    public async Task<User> GetUserById(int id) => await client.GetFromJsonAsync<User>($"users/{id}");
}
