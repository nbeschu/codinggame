using System.Collections.Generic;

namespace CodingGame.Model
{
    public class CharacterCharacteristic
    {
        /** Armor of the character. All characters start with the same amount of heal points, but they have different armors. */
        public long armor { get; set; }

        /** Name of the character */
        public string name { get; set; }

        /** List of the available actions for this character */
        public List<Action> actions { get; set; }
    }
}
