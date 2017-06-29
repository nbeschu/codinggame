using System.Collections.Generic;

namespace CodingGame.Model
{
    public class Game
    {
        /// <summary>
        /// Token of the game
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Is the game started ?
        /// </summary>
        public GameStatus Status { get; set; }

        /// <summary>
        /// Speed of the game (number of milliseconds in a time unit)
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// If status is {@link GameStatus#PLAYING}, indicates the time (in ms) until the game starts
        /// </summary>
        public int? CountDown { get; set; }

        /// <summary>
        ///  Data of the current player
        /// </summary>
        public Player Me { get; set; }

        /// <summary>
        /// Data of the foe of the current player
        /// </summary>
        public Player Foe { get; set; }
    }
}
