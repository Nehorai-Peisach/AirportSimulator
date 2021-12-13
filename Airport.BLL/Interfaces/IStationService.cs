using Airport.Models;
using System.Collections.Generic;

namespace Airport.BLL.Interfaces
{
    public interface IStationService
    {
        void Update(Station station);
        List<Station> GetAll();
        void Add(Station station);
    }
}
