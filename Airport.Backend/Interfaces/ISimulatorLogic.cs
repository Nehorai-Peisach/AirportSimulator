using Airport.Models;
using System.Collections.Generic;

namespace Airport.Backend.Interfaces
{
    public interface ISimulatorLogic
    {
        List<Plane> Planes { get; }
        List<Station> Stations { get; }

        string DepartPlane(Plane plane);
        string LandPlane(Plane plane);
    }
}
