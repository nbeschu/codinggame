using CodingGame.Model;
using System;

namespace CodingGame
{
    class Program
    {
        static void Main(string[] args)
        {

            Game game = new Game();
            game.token = "toto";
            var token = game.token;

            Console.WriteLine(string.Format("token = {0} et game.token = {1}", token, game.token));
            Console.ReadLine();
        }
    }
}
