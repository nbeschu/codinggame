namespace CodingGame.Model
{
    public class ActionHistory
    {
        /// <summary>
        /// The action done by the foe
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// Time in ms since when the action is done by the foe
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Action Id done by the foe (in order to track the actions in the history)
        /// </summary>
        public int Id { get; set; }
    }
}