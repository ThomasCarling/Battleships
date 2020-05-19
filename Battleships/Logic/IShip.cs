using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Logic
{

    /// <summary>
    /// Interface encapsulating the behaviour of a ship, from the
    /// perspective of a player.
    /// </summary>
    public interface IShip
    {

        /// <summary>
        /// Invoked when this ship has been destroyed.
        /// </summary>
        event EventHandler HasBeenSunk;

        /// <summary>
        /// Returns true if ship is at the provided coordinates,
        /// and false otherwise.
        /// </summary>
        bool IsLocatedAt((int, int) coordinates);

        /// <summary>
        /// creates a new identical instance of the ship.
        /// </summary>
        IShip Clone();

        /// <summary>
        /// Indicates the specified coordinates of the ship
        /// have been hit.
        /// </summary>
        void Hit((int, int) coordinates);
    }
}
