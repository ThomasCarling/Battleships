using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Logic
{

    /// <summary>
    /// When implemented, allows a class to perform as a player
    /// of the game of battleships.
    /// </summary>
    public abstract class BasePlayer
    {

        #region Private Fields

        private static readonly (int, int) _oceanSize = (10, 10);
        private readonly IList<IShip> _allShips;
        private IList<IShip> _unsunkShips;

        #endregion

        #region Public Events

        /// <summary>
        /// Event called when this player attacks the other player.
        /// </summary>
        public event EventHandler<CoordinateEventArgs> AttackEnemy;

        /// <summary>
        /// Event called when one of this player's ships have been
        /// hit.
        /// </summary>
        public event EventHandler<CoordinateEventArgs> HasBeenHit;

        /// <summary>
        /// Event called if this player loses the game.
        /// </summary>
        public event EventHandler IveLost;

        #endregion

        #region Constructor

        public BasePlayer(IList<IShip> typesOfShip)
        {
            _allShips = new List<IShip>(typesOfShip.Select(e => e.Clone()));
            _unsunkShips = new List<IShip>(_allShips);

            foreach (IShip ship in _allShips)
                ship.HasBeenSunk += OnShipSunk;

            PlaceShips(_allShips);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when this player should move.
        /// </summary>
        public void Move() => AttackEnemy(this, 
            new CoordinateEventArgs(GetAttemptCoordinates()));

        /// <summary>
        /// Method called when this player is attacked.
        /// </summary>
        /// <param name="source">The attacking player.</param>
        /// <param name="arg">Class containing the coordinates attacked.</param>
        public void OnAttack(Object source, CoordinateEventArgs arg)
        {
            IShip myShip = _unsunkShips.Where(s=> s.IsLocatedAt(arg.Coordinates))
                                       .First();

            myShip?.Hit(arg.Coordinates);
        }

        /// <summary>
        /// Called when this player has hit another player.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public abstract void OnHit(Object source, CoordinateEventArgs args);

        /// <summary>
        /// Called when this player has won the game.
        /// </summary>
        public abstract void OnVictory(Object source, EventArgs args);

        #endregion

        #region Protected Helper Methods

        /// <summary>
        /// Returns the coordinates this player has chosen to 
        /// attack.
        /// </summary>
        protected abstract (int, int) GetAttemptCoordinates();

        /// <summary>
        /// Tells each of the ships where they should
        /// be placed.
        /// </summary>
        protected abstract void PlaceShips(IList<IShip> ships);

        #endregion

        #region Private Helper Methods

        private void OnShipSunk(object src, EventArgs e)
        {
            _unsunkShips.Remove((IShip)src);

            if (_unsunkShips.Count <= 0)
                IveLost(this, EventArgs.Empty);
        }

        #endregion
    }
}
