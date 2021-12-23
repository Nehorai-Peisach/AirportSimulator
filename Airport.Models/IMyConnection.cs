using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models
{
    public interface IMyConnection
    {
        void InvokeAsync(string function, object obl = default);
    }
}
