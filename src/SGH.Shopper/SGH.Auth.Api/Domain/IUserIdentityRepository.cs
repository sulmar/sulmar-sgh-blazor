namespace SGH.Auth.Api.Domain;

public interface IUserIdentityRepository
{
    UserIdentity GetByUsername(string username);
    UserIdentity? GetByRefreshToken(string refreshToken);
}
