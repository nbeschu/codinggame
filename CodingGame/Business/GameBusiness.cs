using CodingGame.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading;

namespace CodingGame.Business
{
    /// <summary>
    /// The fight API REST business manager
    /// </summary>
    public class GameBusiness
    {
        /// <summary>
        /// URL of the coding game server
        /// </summary>
        private static string API_URL = "https://coding-game.swat-sii.fr/api";

        /// <summary>
        /// The client to do the REST calls
        /// </summary>
        public static RestClient client = new RestClient(API_URL); 

        // change this bool to show json response in console
        private static bool ENABLE_JSON_LOG = true;

        private static string CREATE_GAME_URL = "/fights";
        private static string JOIN_GET_GAME_URL = "/fights/{0}/players/{1}";
        private static string PLAY_GAME_URL = "/fights/{0}/players/{1}/actions/{2}";

        #region public methods

        /// <summary>
        /// create a new game
        /// </summary>
        /// <param name="gameName">Game name</param>
        /// <param name="speedy">true to speedup the game, false instead</param>
        /// <param name="versus">true to fight a real player, false to fight the AI</param>
        /// <returns>The newly crated game</returns>
        public static Game CreateGame(string gameName, bool speedy, bool versus)
        {
            JsonObject jsonRequest = new JsonObject();
            jsonRequest.Add("name", gameName);
            jsonRequest.Add("speedy", speedy);
            jsonRequest.Add("versus", versus);

            var request = new RestRequest(CREATE_GAME_URL, Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Accept", "application/json");    
            request.AddJsonBody(jsonRequest);

            var response = client.Execute(request);

            return ConvertAndLogResponse(response, "CREATE");
        }

        /// <summary>
        /// Join an existing game
        /// </summary>
        /// <param name="gameToken">The game token</param>
        /// <param name="playerKey">The player id</param>
        /// <param name="character">The character class</param>
        /// <param name="name">The character name</param>
        /// <returns>The current Game state</returns>
        public static Game JoinGame(string gameToken, string playerKey, string character, string name) 
        {
            JsonObject jsonRequest = new JsonObject();
            jsonRequest.Add("character", character);
            jsonRequest.Add("name", name);

            var request = new RestRequest(string.Format(JOIN_GET_GAME_URL, gameToken, playerKey), Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(jsonRequest);

            var response = client.Execute(request);

            return ConvertAndLogResponse(response, "JOIN");
        }
        
        /// <summary>
        /// Get the current game state
        /// </summary>
        /// <param name="gameToken">The game token</param>
        /// <param name="playerKey">the player id</param>
        /// <returns>The current game state</returns>
        public static Game GetGame(string gameToken, string playerKey)
        {
            var request = new RestRequest(string.Format(JOIN_GET_GAME_URL, gameToken, playerKey), Method.GET);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return ConvertAndLogResponse(response, "GET");
        }

        /// <summary>
        /// Do an action in the game
        /// </summary>
        /// <param name="gameToken">The game token</param>
        /// <param name="playerKey">The player id</param>
        /// <param name="actionName">The action to perform in the game</param>
        /// <returns>The current game state</returns>
        public static Game Play(string gameToken, string playerKey, string actionName)
        {
            Console.WriteLine(actionName + " !");

            var request = new RestRequest(string.Format(PLAY_GAME_URL, gameToken, playerKey, actionName), Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return ConvertAndLogResponse(response, "PLAY");
        }

        /// <summary>
        /// Do an action in the game and wait until the next action is possible
        /// </summary>
        /// <param name="gameToken">The game token</param>
        /// <param name="playerKey">The player id</param>
        /// <param name="actionName">The action to perform in the game</param>
        /// <returns>The action that have been done</returns>
        public static string PlayAndWaitCoolDown(string gameToken, string playerKey, string actionName)
        {
            Game game = Play(gameToken, playerKey, actionName);
            WaitCoolDown(game, actionName);

            return actionName;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Get and log the server response 
        /// </summary>
        /// <param name="response">The server response</param>
        /// <param name="requestType">The type of request sent</param>
        /// <returns></returns>
        private static Game ConvertAndLogResponse(IRestResponse response, string requestType)
        {
            if (ENABLE_JSON_LOG)
            {
                Console.WriteLine();
                Console.WriteLine("Response from " + requestType + " game request :");
                Console.WriteLine(response.Content);
            }

            return JsonConvert.DeserializeObject<Game>(response.Content);
        }

        /// <summary>
        /// Wait until the next action is possible
        /// </summary>
        /// <param name="game">The current game</param>
        /// <param name="actionName">The action performed</param>
        private static void WaitCoolDown(Game game, string actionName)
        {
            if (game.Me != null)
            {
                var action = game.Me.Character.Actions.Find(x => x.Name == actionName);
                if (action != null)
                {
                    try
                    {
                        if (action.CoolDown > 0)
                        {
                            var waitTime = (int)Math.Ceiling((decimal)(action.CoolDown * game.Speed));
                            Console.WriteLine("Waiting cooldown during " + waitTime + "ms...");
                            Thread.Sleep(waitTime);
                        }
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        #endregion
    }
}
