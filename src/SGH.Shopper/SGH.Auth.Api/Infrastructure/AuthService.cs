using Microsoft.AspNetCore.Identity;
using SGH.Auth.Api.Domain;

namespace SGH.Auth.Api.Infrastructure;

public class AuthService : IAuthService
{
    private readonly IUserIdentityRepository userIdentityRepository;
    private readonly IPasswordHasher<UserIdentity> passwordHasher;

    // dotnet add package Microsoft.Extensions.Identity.Core

    public AuthService(IUserIdentityRepository userIdentityRepository, IPasswordHasher<UserIdentity> passwordHasher)
    {
        this.userIdentityRepository = userIdentityRepository;
        this.passwordHasher = passwordHasher;
    }

    public bool TryAuthorize(string username, string password, out UserIdentity userIdentity)
    {
        userIdentity = userIdentityRepository.GetByUsername(username);

        return userIdentity != null && passwordHasher.VerifyHashedPassword(userIdentity, userIdentity.HashedPassword, password) == PasswordVerificationResult.Success;
    }
}
