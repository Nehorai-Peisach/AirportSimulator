using Airport.Models;
using System.Threading.Tasks;

namespace Airport.Backend.Interfaces
{
    public interface ISimulatorServerHub
    {
        Task LandPlane(string planeName = "autoPlane");
        Task DepartPlane(Plane plane);
        Task GetPlanes();
    }
}
