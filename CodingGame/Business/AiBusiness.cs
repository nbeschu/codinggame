using CodingGame.Model;
using System.Linq;

namespace CodingGame.Business
{
    public class AiBusiness
    {
        private const string HIT = "HIT";
        private const string THRUST = "THRUST";
        private const string HEAL = "HEAL";
        private const string SHIELD = "SHIELD";

        /// <summary>
        /// Decide the next action to play
        /// </summary>
        /// <param name="game">The current game state</param>
        /// <returns>The action to play</returns>
        public static string GetNextAction(Game game, string myLastAction)
        {
            var foe = game.Foe;
            var foeHistory = foe.History;

            if (foeHistory != null && foeHistory.Count > 0)
            {
                var lastAction = foeHistory.First();

                ///////////////////////////////
                // If the foe is in cooldown //
                ///////////////////////////////
                if (IsInCooldown(lastAction, game.Speed))
                {
                    // In cooldown with an action != SHIELD
                    // Or the last action is shield but the foe is not behind his shield
                    // => HIT
                    if (lastAction.Action.Name != SHIELD || !bool.Parse(foe.IsBehindShield))
                    {
                        return HIT;
                    }
                    // The foe is behind his shield waiting to get hit
                    return THRUST;
                }
                
                //////////////////////////////////////////////////////////
                // The foe is not in cooldown and no new action is done //
                //////////////////////////////////////////////////////////

                // If the foe has no armor -> can't do SHIELD => HIT
                if (foe.Armor <= 0)
                {
                    return HIT;
                }

                // The foe is behind his shield -> he can HIT anytime
                if (bool.Parse(foe.IsBehindShield))
                {
                    // if my last action is SHIELD -> the foe is still behind his shield => THRUST
                    if (myLastAction == SHIELD)
                    {
                        return THRUST;
                    }
                    // => The foe can attack anytime -> try to bait the foe => SHIELD
                    return SHIELD;
                }

                // The foe is not behind his shield
                // If the last action is shield -> the foe is probably stun from a previous THRUST => HIT
                if (lastAction.Action.Name == SHIELD)
                {
                    return HIT;
                }

                // The foe can SHIELD or HIT anytime => SHIELD
                // By default => HIT
                return HIT;
            }           

            // No history => HIT
            return HIT;
        }

        /// <summary>
        /// Is the foe in cooldown ?
        /// </summary>
        /// <param name="lastAction">The last action done</param>
        /// <param name="gameSpeed">the game speed</param>
        /// <returns>true if in cooldown, false otherwise</returns>
        private static bool IsInCooldown(ActionHistory lastAction, int gameSpeed)
        {
            switch (lastAction.Action.Name)
            {
                case HIT:
                case THRUST:
                case HEAL:
                    return ((lastAction.Action.CoolDown * gameSpeed) - lastAction.Age <= 0);
                case SHIELD:
                    return (lastAction.Age < 500);
                default:
                    return false;

            }
        }
    }
}
