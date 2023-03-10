using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using SGH.Shopper.ClientApp.Models;
using SGH.Shopper.ClientApp.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SGH.Shopper.ClientApp;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IStorageProvider storageProvider;
    private readonly AuthApiService api;

    public CustomAuthenticationStateProvider(IStorageProvider storageProvider, AuthApiService api)
    {
        this.storageProvider = storageProvider;
        this.api = api;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string token = storageProvider.GetItem("token");

        Console.WriteLine(token);

        // dotnet add package System.IdentityModel.Tokens.Jwt
        var identity = new ClaimsIdentity();

        var tokenHandler = new JwtSecurityTokenHandler();

        if (tokenHandler.CanReadToken(token))
        {
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            try
            {
                string secretKey = "your-256-bit-secret";

                var key = System.Text.Encoding.UTF8.GetBytes(secretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidIssuer = "http://sgh.pl",
                    ValidateAudience = false,
                    ValidAudience = "http://domain.com",
                    ClockSkew = TimeSpan.Zero, 
                    ValidateLifetime = true,
                }, out var validatedToken);

                identity = new ClaimsIdentity(jwtSecurityToken.Claims, "SGH Auth");
            }
            catch (Exception ex)
            {
                await LogoutAsync();
            }


        }

        var principal = new ClaimsPrincipal(identity);

        return new AuthenticationState(principal);

    }

    public async Task LoginAsync(LoginRequest request)
    {
        string token = await api.CreateTokenAsync(request);

        Console.WriteLine(token);

        storageProvider.SetItem("token", token);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        storageProvider.SetItem("token", string.Empty);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
