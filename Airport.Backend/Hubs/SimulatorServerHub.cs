using Airport.Backend.Interfaces;
using Airport.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport.Backend.Hubs
{
    public class SimulatorServerHub : Hub
    {
        ISimulatorLogic logic;
        public SimulatorServerHub(ISimulatorLogic logic) => this.logic = logic;

        public async Task LandPlane(string planeName)
        {
            var plane = new Plane() { PlaneName = planeName };
            var message = logic.LandPlane(plane);
            await Clients.All.SendAsync("LandPlane", message);
        }
        public async Task DepartPlane(Plane plane)
        {
            var message = logic.DepartPlane(plane);
            await Clients.All.SendAsync("DepartPlane",message);
        }
        public async Task GetPlanes() => await Clients.All.SendAsync("GetPlanes", logic.Planes);
    }
}
