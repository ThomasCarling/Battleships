using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Logic
{

    public class CoordinateEventArgs : EventArgs
    {
        public (int, int) Coordinates { get; }
        
        public CoordinateEventArgs((int, int) coordinates) : base()
        {
            Coordinates = coordinates;
        }
    }
}
