using Airport.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Airport.Backend.Hubs
{
    public class SimulatorHub : Hub
    {
        public async Task LandPlain(Station station, Plain plain)
        {

        }
    }
}
