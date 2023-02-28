using Bogus;
using Microsoft.AspNetCore.Mvc;
using SGH.Shopper.Domain;
using SGH.Shopper.Infrastructure;
using SGH.Shopper.Infrastructure.Fakers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Faker<Product>, ProductFaker>();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<IEnumerable<Product>>(sp =>
{
    var faker = sp.GetRequiredService<Faker<Product>>();

    return faker.Generate(100);
});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7141");
        policy.WithMethods(new string[] { "GET" });
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/products", async (IProductRepository repository) => await repository.GetAllAsync());

// GET api/products/search?filter={content}

app.MapGet("/api/products/search", async (IProductRepository repository, [FromQuery(Name = "filter")] string content) => await repository.GetByContent(content));


app.MapGet("/api/products/{id:int}", async (IProductRepository repository, int id) => await repository.GetByIdAsync(id));

app.Run();