using Airport.Backend.Interfaces;
using Airport.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace Airport.Backend.Methods
{
    public class ServerToSimulator: IServerToSimulator
    {
        static HubConnection connection;
        IMyConnection myConnection;
        public ServerToSimulator()
        {
            if (connection == null)
            {
                connection = new HubConnectionBuilder()
                .WithUrl(MyConnectionStrings.Server)
                .Build();
            }

            var tmp = new MyConnection();
            tmp.HubConnection = connection;
            myConnection = tmp;
            connection.StartAsync().Wait();
        }

        public IMyConnection Current { get { return myConnection; } }
    }
}
