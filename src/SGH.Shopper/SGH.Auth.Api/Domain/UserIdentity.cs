namespace SGH.Auth.Api.Domain;

public class UserIdentity
{
    public string Username { get; set; }
    public string HashedPassword { get; set; }

    public string Phone { get; set; }
    public string Email { get; set; }

    public string[] Roles { get; set; }

    public string RefreshToken { get; set; }
}
