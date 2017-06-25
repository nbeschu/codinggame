namespace CodingGame.Model
{
    public class Game
    {
        /** Token of the game */
        public string Token { get; set; }

        /** Is the game started? */
        public GameStatus Status { get; set; }

        /** Speed of the game (number of milliseconds in a time unit) */
        public int Speed { get; set; }

        /** If status is {@link GameStatus#PLAYING}, indicates the time (in ms) until the game starts */
        public int CountDown { get; set; }

        /** Data of the current player */
        public Player Me { get; set; }

        /**>Data of the foe of the current player */
        public Player Foe { get; set; }
    }
}
