namespace CodingGame.Model
{
    /// <summary>
    /// The Action in the history (only 10 actions are report in the history at a time)
    /// </summary>
    public class Action
    {
        /// <summary>
        /// Name of the action (to be used by the players to play)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the action (to be read by players to understand the action: players can't see real effects)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Time before the player can't play again after this action (in time units, can't be null)
        /// </summary>
        public long CoolDown { get; set; }

    }
}
