using Airport.Models;
using System.Collections.Generic;

namespace Airport.DAL.Interfaces
{
    public interface IStationRepository
    {
        void Add(Station station);
        void Remove(Station station);
        void Update(Station station);
        Station Get(string id);
        List<Station> GetAll();
    }
}
