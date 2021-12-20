using Airport.Simulator;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirPort.UnitTests.Simulator
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void Method_Senario_ExpectedBehavior()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void Current_Get_ReturnsHubConnectionInConnectedState()
        {
            //Arrange
            var logic = new Logic();

            //Act
            var result = logic.Connection.Current;

            //Assert
            Assert.IsTrue(result.State == HubConnectionState.Connecting);
        }
    }
}