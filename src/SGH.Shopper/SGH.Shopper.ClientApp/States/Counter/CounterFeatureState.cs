using Fluxor;

namespace SGH.Shopper.ClientApp.States.Counter;

public class CounterFeatureState : Feature<CounterState>
{
    public override string GetName() => nameof(CounterState);

    protected override CounterState GetInitialState() => new CounterState { CounterValue = 0 };
}