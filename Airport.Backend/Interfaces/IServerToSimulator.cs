using Microsoft.AspNetCore.SignalR.Client;

namespace Airport.Backend.Interfaces
{
    public interface IServerToSimulator
    {
        HubConnection Current { get; }
    }
}
