using Fluxor;
using SGH.Shopper.ClientApp.Services;

namespace SGH.Shopper.ClientApp.States.User;

public class GetUsersAction
{
}


public class GetUsersResultAction
{
    public IEnumerable<Domain.User> Users { get; }

    public GetUsersResultAction(IEnumerable<Domain.User> users)
    {
        Users = users;
    }
}


public class Reducers
{
    [ReducerMethod]
    public static UserState ReduceGetUsersAction(UserState state, GetUsersAction action) => new UserState(isLoading: true, users: null);

    [ReducerMethod]
    public static UserState ReduceGetUsersResultAction(UserState state, GetUsersResultAction action) => new UserState(isLoading: false, users: action.Users);
}


public class Effects
{
    private readonly JsonPlaceholderService Api;

    public Effects(JsonPlaceholderService api)
    {
        Api = api;
    }

    [EffectMethod]
    public async Task HandleGetUsersAction(GetUsersAction action, IDispatcher dispatcher)
    {

        var users = await Api.GetUsers();

        await Task.Delay(TimeSpan.FromSeconds(3));

        if (users is not null)
        {
            dispatcher.Dispatch(new GetUsersResultAction(users));
        }
    }
}