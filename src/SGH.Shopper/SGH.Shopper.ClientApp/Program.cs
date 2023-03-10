using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SGH.Shopper.ClientApp;
using SGH.Shopper.ClientApp.Services;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// dotnet add package Microsoft.Extensions.Http
builder.Services.AddHttpClient<JsonPlaceholderService>(sp => sp.BaseAddress = new Uri("https://jsonplaceholder.typicode.com"));
builder.Services.AddHttpClient<NasaService>(sp => sp.BaseAddress = new Uri("https://api.nasa.gov"));
builder.Services.AddHttpClient<ProductApiService>(sp => sp.BaseAddress = new Uri("http://localhost:5041"));
builder.Services.AddHttpClient<CustomerApiService>(sp => sp.BaseAddress = new Uri("http://localhost:5041"));


//builder.Services.AddSingleton<HubConnection>(_ =>
//{
//    HubConnection connection = new HubConnectionBuilder()
//                                    .WithUrl("http://localhost:5041/hubs/products")
//                                    .Build();
//    return connection;
//});

builder.Services.AddSingleton<HubConnection>(_ =>new HubConnectionBuilder()
                                    .WithUrl("http://localhost:5041/hubs/products")
                                    .WithAutomaticReconnect()
                                    .Build());

builder.Services.AddSingleton(sp => (IJSInProcessRuntime)sp.GetRequiredService<IJSRuntime>());
//builder.Services.AddSingleton<IStorageProvider, LocalStorageProvider>();
builder.Services.AddSingleton<IStorageProvider, SessionStorageProvider>();

builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
    options.UseReduxDevTools(rdt =>
    {
        rdt.Name = "Shopper App";
    });
});

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();

builder.Services.AddHttpClient<AuthApiService>(sp => sp.BaseAddress = new Uri("https://localhost:7274"));

await builder.Build().RunAsync();
