using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Net;

namespace SGH.Shopper.ClientApp
{
    public class ProductHubConnection : HubConnection
    {
        public ProductHubConnection(IConnectionFactory connectionFactory, IHubProtocol protocol, EndPoint endPoint, IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IRetryPolicy reconnectPolicy) : base(connectionFactory, protocol, endPoint, serviceProvider, loggerFactory, reconnectPolicy)
        {
        }
    }
}
