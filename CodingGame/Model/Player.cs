namespace CodingGame.Model
{
    public class Player
    {
        /** Heal points remaining */
        public long healthPoints { get; set; }

        /** Armor remaining */
        public long armor { get; set; }

        /** Character chosen by the player */
        public CharacterCharacteristic character { get; set; }
    }
}
