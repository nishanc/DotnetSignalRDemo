using Microsoft.AspNetCore.SignalR;

namespace ServerApp.SignalR;

public class NotificationHub : Hub<INotificationHub>
{
    public async Task SendNotificationToUserAsync(string message)
    {
        await Clients.All.SendNotificationAsync(message);
    }

    public async Task SendNotificationToGroupAsync(string group, string message)
    {
        await Clients.Group(group).SendNotificationAsync(message);
    }

    public async Task AddToGroupAsync(string group)
    {
        await this.Groups.AddToGroupAsync(
            this.Context.ConnectionId, group);
    }
}