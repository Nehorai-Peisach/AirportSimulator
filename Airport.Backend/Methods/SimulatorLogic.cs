using Airport.Backend.Interfaces;
using Airport.BLL.Interfaces;
using Airport.Models;
using Airport.Models.DataStructures;
using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Airport.Backend.Methods
{
    public class SimulatorLogic : ISimulatorLogic
    {
        IStationService stationService;
        IPlaneService planeService;
        IServerToClient connectionToClient;
        IServerToSimulator connectionToSimulator;
        Random random = new Random();
        List<Station> stations;
        Graph<Station> landGraph;
        Graph<Station> departGraph;

        public SimulatorLogic(IStationService stationService, IPlaneService planeService, IServerToClient connectionToClient, IServerToSimulator connectionToSimulator)
        {
            this.stationService = stationService;
            this.planeService = planeService;
            this.connectionToClient = connectionToClient;
            this.connectionToSimulator = connectionToSimulator;

            stations = CreateStations(8);
            landGraph = CreateLandGraph(stations);
            departGraph = CreateDepartGraph(stations);
        }

        public List<Plane> Planes { get { return planeService.GetAll(); } }

        List<Station> CreateStations(int num)
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
            else list.ForEach(station => station.Plane = default);

            return list;
        }
        Graph<Station> CreateLandGraph(List<Station> list)
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
        Graph<Station> CreateDepartGraph(List<Station> list)
        {
            if (list.Count < 9) return null;

            var graph = new Graph<Station>();
            graph.AddNode(list[0]);
            graph.AddNode(list[6], list[7]);
            graph.AddNode(list[8]);
            graph.AddNode(list[4]);
            return graph;
        }

        public string DepartPlane(Plane plane)
        {
            if (planeService.GetAll().Count < 1) return $"Can't find {plane}, the garage is empty!";

            var planeInDb = planeService.GetByName(plane.PlaneName);
            if (planeInDb == default) return $"Can't find {plane} in the garage!";

            var tokenSource = new CancellationTokenSource();
            departGraph.First.ForEach(stationNode => new Task(() => NextStation(stationNode, planeInDb, tokenSource)).Start());

            planeService.Remove(plane);
            connectionToSimulator.Current.InvokeAsync("GetPlanes");
            return $"{planeInDb} start depart now.";
        }
        public string LandPlane(Plane plane)
        {
            var tokenSource = new CancellationTokenSource();
            landGraph.First.ForEach(stationNode => new Task(() => NextStation(stationNode, plane, tokenSource)).Start());
            planeService.Add(plane);
            connectionToSimulator.Current.InvokeAsync("GetPlanes");
            return "Land";
        }

        private void NextStation(Node<Station> node, Plane plane, CancellationTokenSource token)
        {
            do
            {
                if (token.IsCancellationRequested) return;
            }
            while (node.Current.Plane != default);

            lock (node.Current.StationLocker)
            {
                if (node.Next == default)
                {
                    node.Current.Plane = default;
                    stationService.Update(node.Current);
                    connectionToClient.Current.InvokeAsync("StationsStatus");
                    token.Cancel();
                    return;
                }

                node.Current.Plane = plane;
                stationService.Update(node.Current);

                node.Current.Plane = default;
                stationService.Update(node.Current);

                token.Cancel();
            }
            connectionToClient.Current.InvokeAsync("StationsStatus");
            Thread.Sleep((int)(node.Current.StationDuration * 1000));

            var tokenSource = new CancellationTokenSource();
            node.Next.ForEach(stationNode => new Task(() => NextStation(stationNode, plane, tokenSource)).Start());
        }
    }
}
