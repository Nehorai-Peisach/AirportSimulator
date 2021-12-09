using Airport.BLL.Interfaces;
using Airport.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Airport.Backend.Hubs
{
    public class SimulatorServerHub : Hub
    {
        ISimulatorLogic logic;
        public SimulatorServerHub(ISimulatorLogic logic) => this.logic = logic;

        public async Task LandPlane(string planeName = "autoPlane")
        {
            var plane = new Plane() { PlaneName = planeName };
            if(logic.LandPlane()) await Clients.All.SendAsync("LandPlane", $"That plane '{plane.PlaneName}', land successfully.");
            else await Clients.All.SendAsync("LandPlane", $"Station 1 taken, '{plane.PlaneName}' can't land now! Please try later.");
        }
        public async Task DeportPlane(Plane plane)
        {
            if(logic.DeportPlane()) await Clients.All.SendAsync("DeportPlane", $"That plane '{plane.PlaneName}', start deporting from the airport!");
            else await Clients.All.SendAsync("DeportPlane", $"Stations 6 & 7 taken, '{plane.PlaneName}' can't deport now! Please try later.");
        }
        public async Task GetPlanes()
        {
            await Clients.All.SendAsync("GetPlanes", logic.Planes);
        }
    }
}
