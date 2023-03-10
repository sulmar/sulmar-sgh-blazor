using Microsoft.AspNetCore.Identity;
using SGH.Auth.Api.Domain;

namespace SGH.Auth.Api.Infrastructure;

public class FakeUserIdentityRepository : IUserIdentityRepository
{
    private readonly IDictionary<string, UserIdentity> users = new Dictionary<string, UserIdentity>();

    public FakeUserIdentityRepository(IPasswordHasher<UserIdentity> passwordHasher)
    {
        users = new Dictionary<string, UserIdentity>()
        {
            ["john"] = new UserIdentity { Username = "john", Email = "john.smith@domain.com", Phone = "555-111-000", DateOfBirth = DateTime.Today.AddYears(-17), Roles = new string[] { "admin" } },
            ["ann"] = new UserIdentity { Username = "ann", Email = "ann.smith@domain.com", Phone = "555-111-000", DateOfBirth = DateTime.Today.AddYears(-18), Roles = new string[] { "paid" } },
            ["kate"] = new UserIdentity { Username = "kate", Email = "kate.smith@domain.com", Phone = "555-111-000", Roles = new string[] { "admin", "paid" } },
            ["bob"] = new UserIdentity { Username = "bob", Email = "bob.smith@domain.com", Phone = "555-111-000", DateOfBirth = DateTime.Today.AddYears(-26), Roles = new string[] { "admin", "paid" } },
        };

        foreach (var user in users.Values)
        {
            user.HashedPassword = passwordHasher.HashPassword(user, "123");
        }
    }

    public UserIdentity? GetByRefreshToken(string refreshToken)
    {
        return users.Values.SingleOrDefault(p => p.RefreshToken == refreshToken);
    }

    public UserIdentity GetByUsername(string username)
    {
        if (users.TryGetValue(username, out var user))
        {
            return user;
        }
        else
            return null;
    }
}
