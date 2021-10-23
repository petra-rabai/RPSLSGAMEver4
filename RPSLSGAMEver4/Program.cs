using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSLSGAMEver4
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard game = new GameBoard();
            Player player = new Player();
            Machine machine = new Machine();
            GameInitialize(game, player, machine);

        }

        public static void GameInitialize(GameBoard game, Player player, Machine machine)
        {
            do
            {
                game.GameWelcomeScreenInitialize(player, machine, game);
                PlayerNavigation(game, player, machine);
                if (player.PlayerChoosedGameMenu == "Start the Game")
                {
                    game.SetPlayerKey(player, game);
                    game.SetMachineKey(machine, game);
                    game.SetPlayerGameItem(player, game);
                    game.SetMachineGameItem(machine, game);
                    game.GameItemsEqualityCheck(player, machine, game);
                    game.SetGameCompareItems(player, machine);
                    game.GameRulesCheck(player, machine, game.GameCompareChoosedItems.Item1, game.GameCompareChoosedItems.Item2);
                    game.GameShowTheResult(player, machine);
                }
                game.WriteGameFinalizeMenuNavigationMessage();
                PlayerNavigation(game, player, machine);
            } while (player.PlayerChoosedGameMenu == "Quit the Game");
        }

        public static void PlayerNavigation(GameBoard game, Player player, Machine machine)
        {
            game.SetPlayerKey(player, game);
            game.GameMenuNavigation(player, machine, game);
        }
    }
}
