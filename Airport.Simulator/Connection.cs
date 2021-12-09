﻿using Airport.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;

namespace Airport.Simulator
{
    class Connection
    {
        static HubConnection connection;
        public Connection(Logic logic)
        {
            if(connection == null)
            {
                connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44325/simulatorServer")
                .Build();

                connection.On("LandPlane", (string massage) => logic.Message = massage);
                connection.On("DeportPlane", (string massage) => logic.Message = massage);
                connection.On("GetPlanes", (List<Plane> planes) => logic.Planes = planes);
            }
        }

        public HubConnection Current { get { return connection; } }
    }
}
