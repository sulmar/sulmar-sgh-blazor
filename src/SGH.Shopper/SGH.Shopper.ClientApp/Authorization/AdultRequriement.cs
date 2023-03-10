using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SGH.Shopper.ClientApp.Authorization;

public class AdultRequirement : IAuthorizationRequirement // mark interface
{
    public int MinimumAge { get; set; }

    public AdultRequirement(int minimumAge = 18)
    {
        MinimumAge = minimumAge;
    }
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

        int minimumAge = requirement.MinimumAge;

        if (context.Resource!=null)
        {
            minimumAge = Convert.ToInt32(context.Resource);
        }

        var dateOfBirth = DateTime.Parse(context.User.FindFirst(ClaimTypes.DateOfBirth).Value);

        var age = DateTime.Today.Year - dateOfBirth.Year;

        if (age >= minimumAge)
        {
            context.Succeed(requirement);
        }
        else
            context.Fail();

        return Task.CompletedTask;

    }
}
