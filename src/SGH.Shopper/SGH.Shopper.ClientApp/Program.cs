using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SGH.Shopper.ClientApp;
using SGH.Shopper.ClientApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// dotnet add package Microsoft.Extensions.Http
builder.Services.AddHttpClient<JsonPlaceholderService>(sp => sp.BaseAddress = new Uri("https://jsonplaceholder.typicode.com"));
builder.Services.AddHttpClient<NasaService>(sp => sp.BaseAddress = new Uri("https://api.nasa.gov"));

await builder.Build().RunAsync();
