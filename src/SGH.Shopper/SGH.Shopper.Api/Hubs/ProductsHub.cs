using Microsoft.AspNetCore.SignalR;

namespace SGH.Shopper.Api.Hubs;

public class ProductsHub : Hub
{
    private readonly ILogger<ProductsHub> logger;

    public ProductsHub(ILogger<ProductsHub> logger)
    {
        this.logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        // zła praktyka
        // logger.LogInformation($"Connected ConnectionId: {Context.ConnectionId}");

        // dobra praktyka
        logger.LogInformation("Connected ConnectionId: {ConnectionId}", Context.ConnectionId);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation("Disconnected ConnectionId: {ConnectionId}", Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}
