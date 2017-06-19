namespace CodingGame.Model
{
    public class Player
    {
        /** Heal points remaining */
        public long HealthPoints { get; set; }

        /** Armor remaining */
        public long Armor { get; set; }

        /** Character chosen by the player */
        public CharacterCharacteristic Character { get; set; }
    }
}
