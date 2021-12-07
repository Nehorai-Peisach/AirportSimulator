using Airport.Models;

namespace Airport.BLL.Interfaces
{
    public interface IStationService
    {
        Plain GetPlain();
        bool LandPlain(Station station, Plain plain);
        bool DeportPlain(Station station);
    }
}
