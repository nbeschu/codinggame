using static CodingGame.Model.Enum;

namespace CodingGame.Model
{
    public class Game
    {
        /** Token of the game */
        public string token { get; set; }

        /** Is the game started? */
        public GameStatus status { get; set; }

        /** Speed of the game (number of milliseconds in a time unit) */
        public int speed { get; set; }

        /** If status is {@link GameStatus#PLAYING}, indicates the time (in ms) until the game starts */
        public long countDown { get; set; }

        /** Data of the current player */
        public Player me { get; set; }

        /**>Data of the foe of the current player */
        public Player foe { get; set; }
    }
}
