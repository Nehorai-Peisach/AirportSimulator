using Airport.Backend.Interfaces;
using Airport.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace Airport.Backend.Methods
{
    public class ServerToSimulator: IServerToSimulator
    {
        static HubConnection connection;
        public ServerToSimulator()
        {
            if (connection == null)
            {
                connection = new HubConnectionBuilder()
                .WithUrl(MyConnectionStrings.Server)
                .Build();
            }

            connection.StartAsync().Wait();
        }

        public HubConnection Current { get { return connection; } }
    }
}
