namespace CodingGame.Model
{
    public class Action
    {
        /** Name of the action (to be used by the players to play) */
        public string Name { get; set; }

        /** Description of the action (to be read by players to understand the action: players can't see real effects) */
        public string Description { get; set; }

        /** Time before the player can't play again after this action (in time units, can't be null) */
        public double CoolDown { get; set; }
    }
}
