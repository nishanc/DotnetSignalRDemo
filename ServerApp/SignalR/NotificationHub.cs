using Microsoft.AspNetCore.SignalR;

namespace ServerApp.SignalR;

public class NotificationHub : Hub<INotificationHub>
{
    public async Task SendNotificationToUserAsync(string message)
    {
        await Clients.All.SendNotificationAsync(message);
    }
}