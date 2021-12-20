using Airport.Backend.Interfaces;
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
                .WithUrl("https://localhost:44325/simulatorServer")
                .Build();
            }

            connection.StartAsync().Wait();
        }

        public HubConnection Current { get { return connection; } }
    }
}
