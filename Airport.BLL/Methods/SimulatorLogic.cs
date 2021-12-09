using Airport.BLL.Interfaces;
using Airport.DAL.Interfaces;
using Airport.Models;
using System;
using System.Collections.Generic;

namespace Airport.BLL.Methods
{
    public class SimulatorLogic : ISimulatorLogic
    {
        IStationRepository stationRepo;
        IPlaneRepository planeRepo;

        Random random;
        Graph<Station> landGraph;
        Graph<Station> departGraph;
        List<Station> stations;

        public SimulatorLogic(IStationRepository stationRepo, IPlaneRepository planeRepo)
        {
            this.stationRepo = stationRepo;
            this.planeRepo = planeRepo;

            random = new Random();
            CreateStations(8);
            CreateLandGraph();
            CreateDepartGraph();
        }

        private void CreateStations(int num)
        {
            stations = new List<Station>() { new Station() { StationName = "Garage" } };
            for (int i = 1; i <= num; i++)
                stations.Add(new Station() { StationName = $"Station{i}", StationDuration = (1 + random.NextDouble() * 9) });
        }
        private void CreateLandGraph()
        {
            landGraph = new Graph<Station>();
            landGraph.AddNode(stations[1]);
            landGraph.AddNode(stations[2]);
            landGraph.AddNode(stations[3]);
            landGraph.AddNode(stations[4]);
            landGraph.AddNode(stations[5]);
            landGraph.AddNode(stations[6], stations[7]);
            landGraph.AddNode(stations[0]);
        }
        private void CreateDepartGraph()
        {
            departGraph = new Graph<Station>();
            departGraph.AddNode(stations[6], stations[7]);
            departGraph.AddNode(stations[8]);
            departGraph.AddNode(stations[4]);
        }

        public List<Plane> Planes { get { return planeRepo.GetAll(); } }

        public bool DepartPlane()
        {
            var lst = planeRepo.GetAll();
            if (lst.Count > 0)
            {

            }
            return true;
        }

        public bool LandPlane(Plane plane)
        {
            planeRepo.Add(plane);
            return true;
        }
    }
}
