using Airport.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.Backend.Interfaces
{
    public interface IServerToClient
    {
        IMyConnection Current { get; }
    }
}
