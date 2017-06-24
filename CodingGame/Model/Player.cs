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
        public long Armor { get; set; }

        /// <summary>
        /// Character chosen by the player
        /// </summary>
        public CharacterCharacteristic Character { get; set; }
    }
}
