namespace SGH.Auth.Api.Domain;

public interface ITokenService
{
    string CreateToken(UserIdentity userIdentity);
    string CreateRefreshToken(UserIdentity userIdentity);
}
