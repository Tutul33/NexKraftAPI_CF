using Microsoft.AspNetCore.SignalR;

namespace API.signalr_hub
{
    public class MessageHub : Hub<IMessageHubClient>
    {
    }
}
