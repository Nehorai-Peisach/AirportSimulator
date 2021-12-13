using Airport.Backend.Interfaces;
using Airport.BLL.Interfaces;
using Airport.Models;
using Airport.Models.DataStructures;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Airport.Backend.Methods
{
    public class SimulatorLogic : ISimulatorLogic
    {
        IStationService stationService;
        IPlaneService planeService;
        ISimulatorClientHub clientHub;

        Random random = new Random();
        List<Station> stations;
        Graph<Station> landGraph;
        Graph<Station> departGraph;

        public SimulatorLogic(IStationService stationService, IPlaneService planeService, ISimulatorClientHub clientHub)
        {
            this.stationService = stationService;
            this.planeService = planeService;
            this.clientHub = clientHub;

            stations = CreateStations(8);
            landGraph = CreateLandGraph(stations);
            departGraph = CreateDepartGraph(stations);
        }

        private List<Station> CreateStations(int num)
        {
            var list = stationService.GetAll();
            if (list.Count == 0)
            {
                list = new List<Station>();
                var garage = new Station() { StationName = "Garage" };
                stationService.Add(garage);

                for (int i = 1; i <= num; i++)
                {
                    var newStation = new Station() { StationName = $"Station{i}", StationDuration = (1 + random.NextDouble() * 9) };
                    list.Add(newStation);
                    stationService.Add(newStation);
                }
            }

            return list;
        }
        private Graph<Station> CreateLandGraph(List<Station> list)
        {
            if (list.Count < 9) return null;

            var graph = new Graph<Station>();
            graph.AddNode(list[1]);
            graph.AddNode(list[2]);
            graph.AddNode(list[3]);
            graph.AddNode(list[4]);
            graph.AddNode(list[5]);
            graph.AddNode(list[6], list[7]);
            graph.AddNode(list[0]);
            return graph;
        }
        private Graph<Station> CreateDepartGraph(List<Station> list)
        {
            if (list.Count < 9) return null;

            var graph = new Graph<Station>();
            graph.AddNode(list[0]);
            graph.AddNode(list[6], list[7]);
            graph.AddNode(list[8]);
            graph.AddNode(list[4]);
            return graph;
        }

        public List<Plane> Planes { get { return planeService.GetAll(); } }

        public string DepartPlane(Plane plane)
        {
            if (planeService.GetAll().Count < 1) return $"Can't find {plane}, the garage is empty!";

            var planeInDb = planeService.GetByName(plane.PlaneName);
            if (planeInDb == default) return $"Can't find {plane} in the garage!";

            foreach (var stationNode in departGraph.First)
            {
                NextStation(stationNode, planeInDb);
            }
            return $"{planeInDb} start depart now.";
        }

        public string LandPlane(Plane plane)
        {
            planeService.Add(plane);
            return "Land";
        }

        private async void NextStation(Node<Station> stationNode, Plane plane)
        {
            await clientHub.StationsStatus();
            Thread.Sleep((int)(stationNode.Current.StationDuration * 1000));

            if (stationNode.Next == default)
            {
                stationNode.Current.Plane = default;
                stationService.Update(stationNode.Current);
                return;
            }

            var flag = false;
            foreach (var node in stationNode.Next)
            {
                node.Current.Semaphore.WaitAsync();
                {
                    while (true) if (node.Current.Plane == default) break;

                    node.Current.Plane = plane;
                    stationService.Update(node.Current);

                    stationNode.Current.Plane = default;
                    stationService.Update(stationNode.Current);

                    NextStation(node, plane);
                    flag = true;
                }
            }

            while (true)
            {
                if (flag)
                {
                    foreach (var node in stationNode.Next)
                        node.Current.Semaphore.Release();
                    break;
                }
            }

        }

    }
}
