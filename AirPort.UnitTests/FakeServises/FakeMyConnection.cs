using Airport.Models;
using System;

namespace AirPort.UnitTests.FakeServises
{
    class FakeMyConnection : IMyConnection
    {
        public void InvokeAsync(string function, object obl = null)
        {
        }
    }
}
