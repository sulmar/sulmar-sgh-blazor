using Bogus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SGH.Shopper.Api;
using SGH.Shopper.Api.Hubs;
using SGH.Shopper.Domain;
using SGH.Shopper.Infrastructure;
using SGH.Shopper.Infrastructure.Fakers;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Faker<Product>, ProductFaker>();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<IEnumerable<Product>>(sp =>
{
    var faker = sp.GetRequiredService<Faker<Product>>();

    return faker.Generate(100);
});


builder.Services.AddSingleton<Faker<Customer>, CustomerFaker>();
builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();
builder.Services.AddSingleton<IEnumerable<Customer>>(sp => sp.GetRequiredService<Faker<Customer>>().Generate(200));


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(new string[] {
            "http://localhost:5059",
            "https://localhost:7141"
        });
        policy.WithMethods(new string[] { "GET", "PUT" });
        policy.AllowAnyHeader();
        policy.WithExposedHeaders("X-Total-Count");
    });
});

builder.Services.AddSignalR();

// // dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    string secretKey = "your-256-bit-secret";
    var key = System.Text.Encoding.UTF8.GetBytes(secretKey);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidIssuer = "http://sgh.pl",
        ValidateAudience = false,
        ValidAudience = "http://domain.com",
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
    };
});


builder.Services.AddAuthorizationBuilder().AddPolicy("admin_policy", policy =>
policy.RequireRole("admin"));

var app = builder.Build();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Assets")),
    RequestPath = "/documents"
});

  

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/products", async (IProductRepository repository) => await repository.GetAllAsync())
    .RequireAuthorization(); // [Authorization(Roles = "admin")]

// GET api/products/search?filter={content}

app.MapGet("/api/products/search", async (IProductRepository repository, [FromQuery(Name = "filter")] string content) => await repository.GetByContent(content)).RequireAuthorization();

// GET api/products?pageSize={pageSize}&pageNumber={pageNumber}
app.MapGet("/api/products/paging", async (IProductRepository repository, [AsParameters] PagingParameters parameters, HttpResponse response) =>
{
    var result = await repository.GetAllAsync(parameters);

    response.Headers.Add("X-Total-Count", result.TotalCount.ToString());

    return Results.Ok(result.Items);
    
}).RequireAuthorization();


app.MapGet("/api/products/{id:int}", async (IProductRepository repository, int id) => await repository.GetByIdAsync(id))
    .WithName("GetProductById");

app.MapGet("api/documents/{message}", (string message) =>
{
    var stream = new MemoryStream();
    // TODO: generowanie pdf - zapis do strumienia

    stream.Position = 0;

    return Results.File(stream, contentType: MediaTypeNames.Application.Pdf, "document.pdf" );
});

app.MapPut("/api/products/{id:int}", async (IProductRepository repository, int id, Product product) =>
{
    if (id != product.Id)
        return Results.BadRequest();

    await repository.UpdateAsync(product);

    return Results.NoContent();
});

app.MapPost("/api/products", async (Product product, IProductRepository repository, IHubContext<ProductsHub> hubContext) =>
{
    await repository.AddAsync(product);

    await hubContext.Clients.All.SendAsync("AddedProduct", product);

    return Results.CreatedAtRoute("GetProductById", new { Id = product.Id }, product);
});

app.MapGroup("/api/customers")
    .MapCustomers()
    .RequireAuthorization("admin_policy");

app.MapHub<ProductsHub>("hubs/products");


app.Run();
