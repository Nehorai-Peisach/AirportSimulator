using Airport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Airport.BLL.Interfaces
{
    public interface ISimulatorLogic
    {
        List<Plane> Planes { get; }

        bool DepartPlane();
        bool LandPlane(Plane plane);
    }
}
