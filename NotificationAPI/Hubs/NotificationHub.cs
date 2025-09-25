using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DynamicNotification.Api.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var sessionRef = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(sessionRef))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, sessionRef);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // SignalR auto-cleans groups; optional custom logic here.
            await base.OnDisconnectedAsync(exception);
        }
    }
}
