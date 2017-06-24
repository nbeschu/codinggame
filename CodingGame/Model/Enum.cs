﻿namespace CodingGame.Model
{
    public partial class Enum
    {
        /// <summary>
        /// The different game status
        /// </summary>
        public enum GameStatus
        {
            None = 0,
            WAITING = 1,
            PLAYING = 2,
            FINISHED = 3
        }

        /// <summary>
        /// The different character classes
        /// </summary>
        public enum Character
        {
            None = 0,
            WARRIOR = 1,
            PALADIN = 2,
            DRUID = 3,
            SORCERER = 4
        }
    }
}
