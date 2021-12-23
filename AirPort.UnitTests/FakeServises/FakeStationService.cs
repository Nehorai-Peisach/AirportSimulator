using Airport.BLL.Interfaces;
using Airport.Models;
using System.Collections.Generic;
using System.Linq;

namespace AirPort.UnitTests.FakeServises
{
    class FakeStationService : IStationService
    {
        List<Station> stations = new List<Station>();

        public void Add(Station station)
        {
            stations.Add(station);
        }

        public List<Station> GetAll()
        {
            return stations;
        }

        public void Update(Station station)
        {
            var tmp = stations.FirstOrDefault(x => x.StationId == station.StationId);
            tmp = station;
        }
    }
}
