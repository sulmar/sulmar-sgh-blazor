using Microsoft.IdentityModel.Tokens;
using SGH.Auth.Api.Domain;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SGH.Auth.Api.Infrastructure;

// dotnet add package System.IdentityModel.Tokens.Jwt

public class JwtTokenService : ITokenService
{
    public string CreateRefreshToken(UserIdentity userIdentity)
    {
        var randomNumber = new byte[32];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public string CreateToken(UserIdentity userIdentity)
    {
        // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.Name, userIdentity.Username));
        //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userIdentity.Username));
        identity.AddClaim(new Claim(ClaimTypes.HomePhone, userIdentity.Phone));
        identity.AddClaim(new Claim(ClaimTypes.HomePhone, userIdentity.Phone));
        identity.AddClaim(new Claim(ClaimTypes.HomePhone, userIdentity.Phone));

        //identity.AddClaim(new Claim(ClaimTypes.Email, userIdentity.Email));

        foreach (var role in userIdentity.Roles)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
        }
        




        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userIdentity.Username));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, userIdentity.Username));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, userIdentity.Username));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.NameId, userIdentity.Username));


        string secretKey = "your-256-bit-secret";
        var key = Encoding.ASCII.GetBytes(secretKey);

        var tokenHandler = new JwtSecurityTokenHandler();
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddMinutes(1),
            SigningCredentials = signingCredentials,
            //Issuer = "http://sgh.pl",
            //Audience = "http://domain.com"
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(securityToken);

        return token;


    }
}
