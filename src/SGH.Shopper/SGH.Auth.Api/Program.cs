using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SGH.Auth.Api.Domain;
using SGH.Auth.Api.Infrastructure;
using SGH.Auth.Api.Models;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IUserIdentityRepository, FakeUserIdentityRepository>();
builder.Services.AddSingleton<IPasswordHasher<UserIdentity>, PasswordHasher<UserIdentity>>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();


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


app.MapPost("/api/token/create", (LoginRequest request, 
    IAuthService authService, ITokenService tokenService, HttpResponse response) =>
{
    if (authService.TryAuthorize(request.Username, request.Password, out var userIdentity))
    {
        var token = tokenService.CreateToken(userIdentity);

        var refreshToken = tokenService.CreateRefreshToken(userIdentity);

        // TODO: przeniesc do repo IUserIdentityRepository
        userIdentity.RefreshToken = refreshToken;

        response.Headers.Add("X-Refresh-Token", refreshToken);

        return Results.Ok(token);

        //return Results.Ok(new
        //{
        //    accesstoken = token,
        //    refreshToken = refreshToken,
        //});
    }

    return Results.BadRequest(new { message = "Username or password is incorrect" });

});


app.MapPost("/api/token/refresh", ([FromBody] string refreshToken, IAuthService authService, ITokenService tokenService, HttpResponse response) =>
{
    if (authService.TryAuthorize(refreshToken, out var userIdentity))
    {
        var token = tokenService.CreateToken(userIdentity);

        response.Headers.Add("X-Refresh-Token", refreshToken);

        return Results.Ok(token);
    }

    return Results.BadRequest(new { message = "Invalid refresh token" });
});

app.Run();
