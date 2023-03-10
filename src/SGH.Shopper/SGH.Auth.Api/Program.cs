using Microsoft.AspNetCore.Identity;
using SGH.Auth.Api.Domain;
using SGH.Auth.Api.Infrastructure;
using SGH.Auth.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserIdentityRepository, FakeUserIdentityRepository>();
builder.Services.AddScoped<IPasswordHasher<UserIdentity>, PasswordHasher<UserIdentity>>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(new string[] {
            "http://localhost:5059",
            "https://localhost:7141"
        });
        policy.WithMethods(new string[] { "POST" });
        policy.AllowAnyHeader();        
    });
});

var app = builder.Build();

app.UseCors();


app.MapGet("/", () => "Hello Auth Api");


app.MapPost("/api/token/create", (LoginRequest request, IAuthService authService, ITokenService tokenService) =>
{
    if (authService.TryAuthorize(request.Username, request.Password, out var userIdentity))
    {
        var token = tokenService.CreateToken(userIdentity);

        return Results.Ok(token);
    }

    return Results.BadRequest(new { message = "Username or password is incorrect" });

});

app.Run();
