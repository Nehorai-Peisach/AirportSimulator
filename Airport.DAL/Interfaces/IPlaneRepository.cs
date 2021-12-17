using Airport.Models;
using System.Collections.Generic;

namespace Airport.DAL.Interfaces
{
    public interface IPlaneRepository
    {
        void Add(Plane plane);
        void Remove(Plane plane);
        List<Plane> GetAll();
        Plane GetByName(string planeName);
    }
}
