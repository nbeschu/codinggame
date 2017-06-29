using System.Collections.Generic;

namespace CodingGame.Model
{
    public class Player
    {
        /// <summary>
        /// Heal points remaining
        /// </summary>
        public long HealthPoints { get; set; }

        /// <summary>
        /// Armor remaining
        /// </summary>
        public int Armor { get; set; }

        /// <summary>
        /// Character chosen by the player
        /// </summary>
        public CharacterCharacteristic Character { get; set; }

        /// <summary>
        /// List of the actions played by the foe so far
        /// </summary>
        public List<ActionHistory> History { get; set; }

        /// <summary>
        /// Is the foe shielded ?
        /// </summary>
        public string IsBehindShield { get; set; }
    }
}
