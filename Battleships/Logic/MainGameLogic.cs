using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Logic
{

    /// <summary>
    /// Class encapsulating all of the logical gameplay for
    /// battleships.
    /// </summary>
    public class MainGameLogic
    {

        /// <summary>
        /// The first game player.
        /// </summary>
        private BasePlayer _playerOne;

        /// <summary>
        /// The second game player.
        /// </summary>
        private BasePlayer _playerTwo;

        /// <summary>
        /// The player who has won, null until a player has declared
        /// their loss.
        /// </summary>
        private bool _noOneHasWon;

        public MainGameLogic(BasePlayer one, BasePlayer two)
        {
            _noOneHasWon = true;
            _playerOne = one;
            _playerTwo = two;

            SetEvents();
        }

        /// <summary>
        /// Method that causes each player to take their turn
        /// in order, until one of them has won.
        /// </summary>
        public void PlayGame(bool startWithPlayerOne)
        {
            if (startWithPlayerOne)
                _playerOne.Move();

            while (_noOneHasWon)
            {
                _playerTwo.Move();
                if (_noOneHasWon)
                    _playerOne.Move();
            }
        }

        /// <summary>
        /// Private helper to assign methods to each player's
        /// events.
        /// </summary>
        private void SetEvents()
        {
            _playerOne.AttackEnemy += _playerTwo.OnAttack;
            _playerTwo.AttackEnemy += _playerOne.OnAttack;
            _playerOne.HasBeenHit += _playerTwo.OnHit;
            _playerTwo.HasBeenHit += _playerOne.OnHit;
            _playerOne.IveLost += (obj, args) => _noOneHasWon = false;
            _playerOne.IveLost += _playerTwo.OnVictory;
            _playerTwo.IveLost += (obj, args) => _noOneHasWon = false;
            _playerTwo.IveLost += _playerOne.OnVictory;
        }
    }
}
