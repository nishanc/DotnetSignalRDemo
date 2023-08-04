namespace ServerApp.SignalR;

public interface INotificationHub
{
    Task SendNotificationAsync(string message);
}