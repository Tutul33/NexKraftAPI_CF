namespace API.signalr_hub
{
    public interface IMessageHubClient
    {
        Task BroadcastMessage(string jsonString);
    }
}
