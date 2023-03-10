using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SGH.Shopper.ClientApp.Authorization;

public class AdultRequirement : IAuthorizationRequirement // mark interface
{
    public int MinimumAge { get; set; } = 18;
}



// Note: pamiętaj o zarejestrowaniu handlera!
public class AdultRequirementHandler : AuthorizationHandler<AdultRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdultRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        var dateOfBirth = DateTime.Parse(context.User.FindFirst(ClaimTypes.DateOfBirth).Value);

        var age = DateTime.Today.Year - dateOfBirth.Year;

        if (age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }
        else
            context.Fail();

        return Task.CompletedTask;

    }
}
