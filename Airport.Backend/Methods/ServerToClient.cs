using Airport.Backend.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace Airport.Backend.Methods
{
    public class ServerToClient : IServerToClient
    {
        static HubConnection connection;
        public ServerToClient()
        {
            if (connection == null)
            {
                connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44325/simulatorClient")
                .Build();
            }

            connection.StartAsync().Wait();
        }

        public HubConnection Current { get { return connection; } }
    }
}
