using Airport.BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Airport.Backend.Hubs
{
    public class SimulatorClientHub : Hub
    {
        IStationService stationService;
        public SimulatorClientHub(IStationService stationService) => this.stationService = stationService;

        public async Task StationsStatus()
        {
            var status = stationService.GetAll();
            await Clients.All.SendAsync("StationsStatus", status);
        }
    }
}