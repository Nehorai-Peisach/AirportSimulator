using Airport.Models;

namespace Airport.BLL.Interfaces
{
    public interface IStationService
    {
        Plane GetPlain();
        bool LandPlain(Station station, Plane plain);
        bool DeportPlain(Station station);
    }
}
