using Airport.Backend.Interfaces;
using Airport.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace Airport.Backend.Methods
{
    public class ServerToClient : IServerToClient
    {
        static HubConnection connection;
        IMyConnection myConnection;
        public ServerToClient()
        {
            if (connection == null)
            {
                connection = new HubConnectionBuilder()
                .WithUrl(MyConnectionStrings.Client)
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
