namespace SGH.Auth.Api.Domain;

public interface IAuthService
{
    // UserIdentity Authorize(string username, string password);

    bool TryAuthorize(string username, string password, out UserIdentity userIdentity);
    bool TryAuthorize(string refreshToken, out UserIdentity userIdentity);  
}
