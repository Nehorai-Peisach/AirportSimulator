using Airport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.DAL.Interfaces
{
    public interface IPlaneRepository
    {
        void Add(Plane plane);
        void Remove(Plane plane);
        List<Plane> GetAll();
    }
}
