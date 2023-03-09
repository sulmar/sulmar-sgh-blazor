using Fluxor;

namespace SGH.Shopper.ClientApp.States.User;

//public record UserState
//{
//    public bool IsLoading { get; init; }
//    public IEnumerable<Domain.User> Users { get; init; }
//}


[FeatureState]
public class UserState
{
    public bool IsLoading { get;  }
    public IEnumerable<Domain.User> Users { get; }

    private UserState() { }

    public UserState(bool isLoading, IEnumerable<Domain.User> users)
    {
        IsLoading = isLoading;
        Users = users ?? Array.Empty<Domain.User>();
    }
}