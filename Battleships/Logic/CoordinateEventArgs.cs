using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Logic
{

    /// <summary>
    /// Event argument containing coordinates.
    /// </summary>
    public class CoordinateEventArgs : EventArgs
    {

        /// <summary>
        /// The coordinates relating to the event.
        /// </summary>
        public (int, int) Coordinates { get; }
        
        public CoordinateEventArgs((int, int) coordinates) : base()
        {
            Coordinates = coordinates;
        }
    }
}
