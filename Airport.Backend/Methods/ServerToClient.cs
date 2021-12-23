using Airport.Backend.Interfaces;
using Airport.Models;
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
                .WithUrl(MyConnectionStrings.Client)
                .Build();
            }

            connection.StartAsync().Wait();
        }

        public HubConnection Current { get { return connection; } }
    }
}
