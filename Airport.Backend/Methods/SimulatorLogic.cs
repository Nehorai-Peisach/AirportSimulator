using Airport.Backend.Interfaces;
using Airport.BLL.Interfaces;
using Airport.Models;
using Airport.Models.DataStructures;
using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System.Linq;

namespace Airport.Backend.Methods
{
    public class SimulatorLogic : ISimulatorLogic
    {
        IStationService stationService;
        IPlaneService planeService;
        IServerToClient connectionToClient;
        IServerToSimulator connectionToSimulator;
        Random random = new Random();
        public List<Plane> Planes { get { return planeService.GetAll(); } }

        public SimulatorLogic(IStationService stationService, IPlaneService planeService, IServerToClient connectionToClient, IServerToSimulator connectionToSimulator)
        {
            this.stationService = stationService;
            this.planeService = planeService;
            this.connectionToClient = connectionToClient;
            this.connectionToSimulator = connectionToSimulator;

            CreateStations(8);
            new Task(() => CreateLandGraph()).Start();
            new Task(() => CreateDepartGraph()).Start();
            connectionToSimulator.Current.InvokeAsync("GetPlanes");
        }


        public List<Station> Stations { get; set; }
        void CreateStations(int num)
        {
            var list = stationService.GetAll();

            if (list.Count == 0)
            {
                list = new List<Station>();
                for (int i = 0; i < num; i++)
                {
                    var newStation = new Station() { StationName = $"Station{i + 1}", StationDuration = new TimeSpan(random.Next(5, 10) * 1000000) };
                    list.Add(newStation);
                    stationService.Add(newStation);
                }
            }
            else list.ForEach(station => station.Plane = default);
            Stations = list;
        }

        Graph<Station> landGraph;
        void CreateLandGraph()
        {
            var biggest = 6;
            do
            {
                if (Stations.Count > biggest)
                {
                    landGraph = new Graph<Station>(GraphType.Landing);
                    landGraph.AddNode(Stations[0]);
                    landGraph.AddNode(Stations[1]);
                    landGraph.AddNode(Stations[2]);
                    landGraph.AddNode(Stations[3]);
                    landGraph.AddNode(Stations[4]);
                    landGraph.AddNode(Stations[5], Stations[biggest]);
                }
            } while (Stations.Count < biggest + 1);

        }

        Graph<Station> departGraph;
        void CreateDepartGraph()
        {
            var biggest = 7;
            do
            {
                if (Stations.Count > biggest)
                {
                    departGraph = new Graph<Station>(GraphType.Departing);
                    departGraph.AddNode(Stations[5], Stations[6]);
                    departGraph.AddNode(Stations[biggest]);
                    departGraph.AddNode(Stations[3]);
                }
            } while (Stations.Count < biggest + 1);
        }

        public string DepartPlane(Plane plane)
        {
            if (planeService.GetAll().Count < 1) return $"Can't find {plane}, the garage is empty!";

            var planeInDb = planeService.GetByName(plane.PlaneName);
            if (planeInDb == default) return $"Can't find {plane} in the garage!";

            var tcs = new TaskCompletionSource();
            departGraph.First.ForEach(stationNode => new Task(() => NextStation(stationNode, plane, tcs)).Start());
            planeService.Remove(plane);
            return $"{planeInDb} start depart now.";
        }
        public string LandPlane(Plane plane)
        {
            var tcs = new TaskCompletionSource();
            landGraph.First.ForEach(stationNode => new Task(() => NextStation(stationNode, plane, tcs)).Start());
            return "Landing..";
        }


        private void NextStation(Node<Station> node, Plane plane, TaskCompletionSource tcs)
        {
            do
            {
                if (tcs.Task.IsCompleted) return;
            } while (node.Value.Plane != default);

            lock (node.Value.Loker)
            {
                lock (tcs)
                {
                    if (tcs.Task.IsCompleted) return;
                    tcs.SetResult();
                }

                //the next station
                {
                    UpdateStation(node.Value, plane);
                    UpdatePlane(plane, node.Value);
                }

                //the current station
                if (node.Previous != default)
                {
                    foreach (var item in node.Previous)
                    {
                        if (item.Value.Plane != default && item.Value.Plane.PlaneId == plane.PlaneId)
                            UpdateStation(item.Value);
                    }
                }
                connectionToClient.Current.InvokeAsync("StationsStatus");
                Thread.Sleep(node.Value.StationDuration);
            }
            if (node.Next == default)
            {
                UpdateStation(node.Value);
                UpdatePlane(plane);

                if (node.type == GraphType.Landing) planeService.Add(plane);
                connectionToClient.Current.InvokeAsync("StationsStatus");
                connectionToSimulator.Current.InvokeAsync("GetPlanes");
            }
            else
            {
                var newTcs = new TaskCompletionSource();
                node.Next.ForEach(stationNode => new Task(() => NextStation(stationNode, plane, newTcs)).Start());
            }
        }

        /// <summary>
        /// Change the 'CurrentStation' state in the plane.
        /// if station is not attached => remove the current station from plane.
        /// </summary>
        /// <param name="station"></param>
        /// <param name="plane"></param>
        private void UpdatePlane(Plane plane, Station station = null)
        {
            if (station != null) plane.CurrentStationName = station.StationName;
            else plane.CurrentStationName = default;
        }

        /// <summary>
        /// Change the 'Plane' state in the station.
        /// if plane is not attached => remove the current plane from station.
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="station"></param>
        private void UpdateStation(Station station, Plane plane = null)
        {
            station.Plane = plane;

            Stations.Find(x => x.StationId == station.StationId).Plane = plane;
            stationService.Update(station);
        }
    }
}
