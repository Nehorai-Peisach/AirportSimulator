using Airport.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace Airport.Backend.Interfaces
{
    public interface IServerToSimulator
    {
        IMyConnection Current { get; }
    }
}
