using Airport.BLL.Interfaces;
using Airport.DAL.Interfaces;
using Airport.Models;

namespace Airport.BLL.Methods
{
    public class MyAirport : IMyAirport
    {
        IStationRepository repo;
        public MyAirport(IStationRepository repo)
        {
            this.repo = repo;
            Create();
        }

        public void Create()
        {
            for (int i = 1; i <= 8; i++)
            {
                repo.Add(new Station() { StationName = $"Station{i}" });
            }
        }
    }
}
