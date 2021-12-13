using Airport.Backend.Interfaces;
using Airport.BLL.Interfaces;
using Airport.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport.Backend.Hubs
{
    public class SimulatorClientHub : Hub, ISimulatorClientHub
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
