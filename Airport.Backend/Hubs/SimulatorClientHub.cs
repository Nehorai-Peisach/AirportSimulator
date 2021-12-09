using Airport.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Airport.Backend.Hubs
{
    public class SimulatorClientHub : Hub
    {
        public async Task LandPlain(Station station, Plane plain)
        {

        }
    }
}
