using System.Collections.Generic;

namespace CodingGame.Model
{
    public class CharacterCharacteristic
    {
        /// <summary>
        /// Armor of the character. All characters start with the same amount of heal points, but they have different armors.
        /// </summary>
        public long Armor { get; set; }

        /// <summary>
        /// Name of the character
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of the available actions for this character
        /// </summary>
        public List<Action> Actions { get; set; }
    }
}
