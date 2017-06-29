using CodingGame.Business;
using CodingGame.Model;
using System;
using System.Threading;

namespace CodingGame
{
    class Program
    {
        // data from main arguments
        private static Mode mode;
        private static string gameName;
        private static string gameToken;
        private static Character character;
        private static string playerName;

        // change this boolean to accelerate the game
        private static bool speedy = false;
        // change this boolean to play against a real player
        private static bool versusPlayer = false;

        private static string playerKey = "nbe";

        /// <summary>
        /// The next action to do
        /// </summary>
        /// <remarks>1st action of the game is HIT</remarks>
        public static string nextAction = "HIT";

        /// <summary>
        /// The last action I have done
        /// </summary>
        public static string lastAction;


        #region main

        /// <summary>
        /// Launch the game
        /// </summary>
        /// <param name="args">the arguments to create/join a game</param>
        public static void Main(string[] args)
        {
            // Check the program arguments
            CheckArguments(args);

            try
            {
                Game game = null;

                // Create a game
                if (mode == Mode.CREATE)
                {
                    Console.WriteLine("Creating the game...");
                    game = GameBusiness.CreateGame(gameName, speedy, versusPlayer);
                    gameToken = game.Token;
                }
                // Then join the game
                Console.WriteLine();
                Console.WriteLine("Joining the game...");
                game = GameBusiness.JoinGame(gameToken, playerKey, character.ToString(), playerName);

                // Wait for the opponent
                if (game.Status == GameStatus.WAITING)
                {
                    Console.WriteLine("Waiting the future victim to join the game...");
                    while (game.Status == GameStatus.WAITING)
                    {
                        Thread.Sleep(500);
                        game = GameBusiness.GetGame(game.Token, playerKey);
                    }
                }

                // Opponent is connected. Waiting the countdown
                game = GameBusiness.GetGame(game.Token, playerKey);
                Console.WriteLine("Waiting countdown during " + game.CountDown + "ms...");
                Thread.Sleep((int)game.CountDown);

                Console.WriteLine();
                Console.WriteLine("Fight !");
                Console.WriteLine();

                // AI playing the game
                game = PlayGame(game);

                Console.WriteLine();
                // game result
                if (game.Me.HealthPoints > 0)
                {
                    Console.WriteLine("You WIN ! :D");
                }
                else
                {
                    Console.WriteLine("You lose... :'(");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Something goes wrong...");
                Console.WriteLine(e.Message);
            }

            QuitApplication();
        }

        #endregion

        #region private methods

        /// <summary>
        /// Game played by our AI
        /// </summary>
        private static Game PlayGame(Game game)
        {
            //Create a callback to get the current game state each 100ms
            Timer timer = new Timer(WatchGame, playerKey, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(100));

            while (game.Status != GameStatus.FINISHED)
            {
                lastAction = GameBusiness.PlayAndWaitCoolDown(game.Token, playerKey, nextAction);

                game = GameBusiness.GetGame(game.Token, playerKey);
                Console.WriteLine("Me: " + game.Me.HealthPoints + "pv, foe: " + game.Foe.HealthPoints + "pv.");
            }

            timer.Dispose();

            return game;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="playerKey"></param>
        /// <param name="nextAction"></param>
        private static void WatchGame(object playerKey)
        {
            var key = playerKey.ToString();

            var game = GameBusiness.GetGame(gameToken, key);
            Console.WriteLine("Me: " + game.Me.HealthPoints + "pv, foe: " + game.Foe.HealthPoints + "pv.");

            nextAction = AiBusiness.GetNextAction(game, lastAction);
        }

        /// <summary>
        /// Check the input arguments. Quit the program if check failed
        /// </summary>
        /// <param name="args">the arguments to check</param>
        private static void CheckArguments(string[] args)
        {
            // check mode argument
            if (args.Length == 0 || (!(Mode.CREATE.ToString() == args[0]) && !(Mode.JOIN.ToString() == args[0])))
            {
                ShowErrorAndQuit("1st argument is required, and must be " + Mode.CREATE + " or " + Mode.JOIN);
            }
            mode = (Mode)Enum.Parse(typeof(Mode), args[0]);

            // check game name/token argument, and versus argument in case of creation mode
            switch (mode)
            {
                case Mode.CREATE:
                    if (args.Length < 2)
                    {
                        ShowErrorAndQuit("2nd argument is required, and must be the game name");
                    }
                    gameName = args[1];

                    if (args.Length > 4 && bool.Parse(args[4]))
                    {
                        versusPlayer = true;
                    }
                    break;
                case Mode.JOIN:
                    if (args.Length < 2)
                    {
                        ShowErrorAndQuit("2nd argument is required, and must be the game token");
                    }
                    gameToken = args[1];
                    break;
                case Mode.None:
                default:
                    ShowErrorAndQuit("Unknown mode. The mode must be " + Mode.CREATE + " or " + Mode.JOIN);
                    break;
            }

            // check character argument
            if (args.Length < 3)
            {
                ShowErrorAndQuit("3rd argument is required, and must be your character type");
            }

            if (!Enum.TryParse(args[2], out character) || !Enum.IsDefined(typeof(Character), character))
            {
                var values = Enum.GetNames(typeof(Character));
                ShowErrorAndQuit("3rd argument must be " + string.Join(" or ", values));
            }

            // check player name argument
            if (args.Length < 4)
            {
                ShowErrorAndQuit("4th argument is required and must be your player name");
            }
            playerName = args[3];

            Console.WriteLine("All yours arguments are OK.");
            Console.WriteLine();
        }

        /// <summary>
        /// Display the error and quit the game
        /// </summary>
        private static void ShowErrorAndQuit(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            Console.WriteLine("How to use in CREATE mode : CodingGame.exe <mode> <gameName> <characterClass> <characterName> <versusPlayer>");
            Console.WriteLine("How to use in JOIN mode : CodingGame.exe <mode> <gameToken> <characterClass> <characterName> <versusPlayer>");

            QuitApplication();
        }

        /// <summary>
        /// Quit the application console
        /// </summary>
        private static void QuitApplication()
        {
            Console.WriteLine();
            Console.Write("Press any key to quit...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        #endregion
    }
}
