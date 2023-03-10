using Microsoft.IdentityModel.Tokens;
using SGH.Auth.Api.Domain;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SGH.Auth.Api.Infrastructure;

// dotnet add package System.IdentityModel.Tokens.Jwt

public class JwtTokenService : ITokenService
{
    public string CreateToken(UserIdentity userIdentity)
    {
        // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.Name, userIdentity.Username));
        //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userIdentity.Username));
        identity.AddClaim(new Claim(ClaimTypes.HomePhone, userIdentity.Phone));
        //identity.AddClaim(new Claim(ClaimTypes.Email, userIdentity.Email));


        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userIdentity.Username));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.UniqueName, userIdentity.Username));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, userIdentity.Username));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.NameId, userIdentity.Username));

        string secretKey = "your-256-bit-secret";

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = credentials,
            Issuer = "http://sgh.pl",
            Audience = "http://domain.com"
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(securityToken);

        return token;


    }
}
