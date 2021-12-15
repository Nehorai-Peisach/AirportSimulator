using Airport.BLL.Interfaces;
using Airport.DAL.Interfaces;
using Airport.Models;
using System.Collections.Generic;

namespace Airport.BLL.Services
{
    public class PlaneService : IPlaneService
    {
        IPlaneRepository repo;
        public PlaneService(IPlaneRepository repo) => this.repo = repo;

        public void Add(Plane plane)
        {
            if (plane != default)
                repo.Add(plane);
        }

        public List<Plane> GetAll()
        {
            return repo.GetAll();
        }

        public Plane GetByName(string planeName)
        {
            if (planeName != default)
                return repo.GetByName(planeName);
            return default;
        }

        public void Remove(Plane plane)
        {
            if (plane != default)
                repo.Remove(plane);
        }
    }
}
