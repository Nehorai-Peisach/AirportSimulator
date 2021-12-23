using Airport.Backend.Methods;
using Airport.Models;
using Airport.Simulator;
using AirPort.UnitTests.FakeServises;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirPort.UnitTests.Backend
{
    [TestClass]
    public class LogicTests
    {
        FakePlaneService planeService;
        FakeServerToClient serverToClient;
        FakeServerToSimulator serverToSimulator;
        FakeStationService stationService;
        void RenewFake()
        {
            planeService = new FakePlaneService();
            serverToClient = new FakeServerToClient();
            serverToSimulator = new FakeServerToSimulator();
            stationService = new FakeStationService();
        }

        [TestMethod]
        public void Method_Senario_ExpectedBehavior()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void DepartPlane_GarageEmpty_ReturnGarageIsEmpty()
        {
            //Arrange
            RenewFake();
            SimulatorLogic logic = new SimulatorLogic(stationService, planeService, serverToClient, serverToSimulator);
            var plane = new Plane() { PlaneName = "test" };

            //Act
            var result = logic.DepartPlane(plane);

            //Assert
            Assert.IsTrue(result == $"Can't find {plane}, the garage is empty!");
        }
        [TestMethod]
        public void DepartPlane_PlaneNotItGarage_ReturnCantFindPlane()
        {
            //Arrange
            RenewFake();
            SimulatorLogic logic = new SimulatorLogic(stationService, planeService, serverToClient, serverToSimulator);
            var plane = new Plane() { PlaneName = "test" };

            //Act
            planeService.Add(new Plane());
            var result = logic.DepartPlane(plane);

            //Assert
            Assert.IsTrue(result == $"Can't find {plane} in the garage!");
        }
        [TestMethod]
        public void CreateStations_ValidPlane_ReturnStartDepart()
        {
            //Arrange
            RenewFake();
            SimulatorLogic logic = new SimulatorLogic(stationService, planeService, serverToClient, serverToSimulator);

            //Act
            var result = logic.Stations.Count;

            //Assert
            Assert.IsTrue(result == 8);
        }
        [TestMethod]
        public void UpdatePlane_PlaneAndStaion_AddTheStationToPlane()
        {
            //Arrange
            RenewFake();
            SimulatorLogic logic = new SimulatorLogic(stationService, planeService, serverToClient, serverToSimulator);
            var plane = new Plane() { PlaneName = "testPlane" };
            var station = new Station() { StationName = "testStation" };

            //Act
            var before = plane.CurrentStationName;
            logic.UpdatePlane(plane,station);
            var after = plane.CurrentStationName;

            //Assert
            Assert.AreNotEqual(before, after);
            Assert.AreEqual(after, station.StationName);
        }
        [TestMethod]
        public void UpdatePlane_PlaneAndNoStaion_RemoveTheStationToPlane()
        {
            //Arrange
            RenewFake();
            SimulatorLogic logic = new SimulatorLogic(stationService, planeService, serverToClient, serverToSimulator);
            var plane = new Plane() { PlaneName = "testPlane", CurrentStationName = "testStation" };

            //Act
            var before = plane.CurrentStationName;
            logic.UpdatePlane(plane);
            var after = plane.CurrentStationName;

            //Assert
            Assert.AreNotEqual(before, after);
            Assert.AreEqual(after, default);
        }
        [TestMethod]
        public void UpdateStation_StaionAndPlane_AddThePlaneToStation()
        {
            //Arrange
            RenewFake();
            SimulatorLogic logic = new SimulatorLogic(stationService, planeService, serverToClient, serverToSimulator);
            var station = new Station() { StationName = "testStation" };
            var plane = new Plane() { PlaneName = "testPlane" };

            //Act
            var before = station.CurrentPlane;
            logic.UpdateStation(station, plane);
            var after = station.CurrentPlane;

            //Assert
            Assert.AreNotEqual(before, after);
            Assert.AreEqual(after, plane);
        }
        [TestMethod]
        public void UpdateStation_StaionAndNoPlane_RemoveThePlaneToStation()
        {
            //Arrange
            RenewFake();
            SimulatorLogic logic = new SimulatorLogic(stationService, planeService, serverToClient, serverToSimulator);
            var plane = new Plane() { PlaneName = "testPlane"};
            var station = new Station() { StationName = "testStation", CurrentPlane = plane };

            //Act
            var before = station.CurrentPlane;
            logic.UpdateStation(station);
            var after = station.CurrentPlane;

            //Assert
            Assert.AreNotEqual(before, after);
            Assert.AreEqual(after, default);
        }
    }
}