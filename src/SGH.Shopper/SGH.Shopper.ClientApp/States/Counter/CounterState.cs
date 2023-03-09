using Fluxor;

namespace SGH.Shopper.ClientApp.States.Counter;


public record CounterState
{
    public int CounterValue { get; init; }
}



// public record GetProductsRequest(decimal priceFrom, decimal priceTo, string color);

/*
public class GetProductsRequest
{
    public decimal PriceFrom { get; set; }
    public decimal PriceTo { get; set; }
    public string Color { get; set; }

    public GetProductsRequest(decimal priceFrom, decimal priceTo, string color)
    {
        PriceFrom = priceFrom;
        PriceTo = priceTo;
        Color = color;
    }
}

*/
