using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models
{
    public class MyConnection : IMyConnection
    {
        public HubConnection HubConnection { get; set; }
        public void InvokeAsync(string function, object obl = null)
        {
            HubConnection.InvokeAsync(function, obl);
        }
    }
}
