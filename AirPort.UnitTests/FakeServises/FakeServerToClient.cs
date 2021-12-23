using Airport.Backend.Interfaces;
using Airport.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirPort.UnitTests.FakeServises
{
    class FakeServerToClient : IServerToClient
    {
        public IMyConnection Current { get { return new FakeMyConnection(); }  }
    }
}
