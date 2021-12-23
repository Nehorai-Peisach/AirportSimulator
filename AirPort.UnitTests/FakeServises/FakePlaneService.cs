using Airport.BLL.Interfaces;
using Airport.Models;
using System.Collections.Generic;
using System.Linq;

namespace AirPort.UnitTests.FakeServises
{
    class FakePlaneService : IPlaneService
    {
        List<Plane> planes = new List<Plane>();

        public void Add(Plane plane)
        {
            planes.Add(plane);
        }

        public List<Plane> GetAll()
        {
            return planes;
        }

        public Plane GetByName(string planeName)
        {
            return planes.FirstOrDefault(x => x.PlaneName == planeName);
        }

        public void Remove(Plane plane)
        {
            planes.Remove(plane);
        }
    }
}
