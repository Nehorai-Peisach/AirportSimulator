using Airport.Backend.Interfaces;
using Airport.Models;

namespace AirPort.UnitTests.FakeServises
{
    class FakeServerToSimulator : IServerToSimulator
    {
        public IMyConnection Current { get { return new FakeMyConnection(); } }

    }
}
