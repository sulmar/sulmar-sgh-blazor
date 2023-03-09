using Fluxor;

namespace SGH.Shopper.ClientApp.States.Counter;

public static class CounterReducers
{
    [ReducerMethod]
    public static CounterState ReduceIncrementCounterAction(CounterState state, IncrementCounterAction action)
    {
        return new CounterState { CounterValue = state.CounterValue + 1 };
    }

    [ReducerMethod]
    public static CounterState ReduceDecrementCounterAction(CounterState state, DecrementCounterAction action) => new() { CounterValue = state.CounterValue - 1 };
}
