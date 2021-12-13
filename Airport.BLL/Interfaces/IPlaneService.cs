using Airport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.BLL.Interfaces
{
    public interface IPlaneService
    {
        List<Plane> GetAll();
        void Add(Plane plane);
        Plane GetByName(string planeName);
    }
}
