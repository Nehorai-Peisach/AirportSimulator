using Airport.BLL.Interfaces;
using Airport.DAL.Interfaces;
using Airport.Models;
using System.Collections.Generic;

namespace Airport.BLL.Methods
{
    public class SimulatorLogic : ISimulatorLogic
    {
        IStationRepository stationRepo;
        IPlaneRepository planeRepo;
        public SimulatorLogic(IStationRepository stationRepo, IPlaneRepository planeRepo)
        {
            this.stationRepo = stationRepo;
            this.planeRepo = planeRepo;
        }

        public List<Plane> Planes { get { return planeRepo.GetAll(); } }

        public bool DeportPlane()
        {
            var lst = planeRepo.GetAll();
            return true;
            //if(lst.Count>0)
        }

        public bool LandPlane(Plane plane)
        {
            planeRepo.Add(plane);
            return true;
        }
    }
}
