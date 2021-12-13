using System.Threading.Tasks;

namespace Airport.Backend.Interfaces
{
    public interface ISimulatorClientHub
    {
        Task StationsStatus();
    }
}
