namespace CodingGame.Model
{
    public class Action
    {
        /** Name of the action (to be used by the players to play) */
        public string name { get; set; }

        /** Description of the action (to be read by players to understand the action: players can't see real effects) */
        public string description { get; set; }

        /** Time before the player can't play again after this action (in time units, can't be null) */
        public double coolDown { get; set; }
    }
}
