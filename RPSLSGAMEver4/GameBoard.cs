using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSLSGAMEver4
{
    public class GameBoard
    {
        readonly Player player = new Player();
        readonly Machine machine = new Machine();

        public string gameResultTimeStamp = DateTime.Now.ToString("\n MM/dd/yyyy h:mm tt\n");

        public string savedResult = "";

        public string gameResult = "";

        public string gameResultDirectory = Properties.Settings.Default.FolderPath;

        public string gameResultFullPath = "";
        private Dictionary<char, string> gameMenu = new Dictionary<char, string>
        {
            ['E'] = "Start the Game",
            ['H'] = "Game Help",
            ['B'] = "Back to the Menu",
            ['C'] = "Save the Result",
            ['Q'] = "Quit the Game"
        };

        private Dictionary<char, string> gameItems = new Dictionary<char, string>
        {
            ['P'] = "Paper",
            ['S'] = "Scissor",
            ['V'] = "Spock",
            ['R'] = "Rock",
            ['L'] = "Lizard"

        };

        public Tuple<string, string> gameCompareChoosedItems;

        public Dictionary<Tuple<string, string>, string> gameWinner = new Dictionary<Tuple<string, string>, string>();

        public Dictionary<char, string> GameMenu { get => gameMenu; set => gameMenu = value; }
        public Dictionary<char, string> GameItems { get => gameItems; set => gameItems = value; }

        public void GameWelcomeScreenInitialize()
        {
            Console.Title = Properties.Resources.gameTitle;
            Console.WriteLine(Properties.Resources.gameWelcomeMessage
                              + "\n"
                              + Properties.Resources.playerWaitForInputMessage);
            player.GetPlayerKey();
            player.GetChoosedPlayerMenu();
            GameMenuNavigation();
        }
        
        public void GameMenuNavigation()
        {
            player.GetChoosedPlayerMenu();
            switch (player.PlayerChoosedGameMenu)
            {
                case "Start the Game":
                    //
                    Console.WriteLine(Properties.Resources.gameAvailableItems);
                    player.GetPlayerPoint();
                    machine.GetMachinePoint();
                    player.GetPlayerKey();
                    machine.GetMachineKey();
                    player.GetChoosedPlayerGameItem();
                    machine.GetChoosedMachineGameItem();
                    GameItemsEqualityCheck();
                    gameCompareChoosedItems = new Tuple<string, string>(player.PlayerChoosedGameItem, machine.MachineChoosedGameItem);
                    break;
                case "Game Help":
                    GameHelp();
                    break;
                case "Back to the Menu":
                    GameWelcomeScreenInitialize();
                    break;
                case "Save the Result":
                    //
                    break;
                case "Quit the Game":
                    GameExit();
                    break;
                default:
                    break;
            }
        }

        public void GameItemsEqualityCheck()
        {
            if (player.PlayerChoosedGameItem == machine.MachineChoosedGameItem)
            {
                Console.WriteLine(Properties.Resources.gameItemsEqualMessage);
                player.NotifyPalyerToAnInvalidAction();
                player.GetPlayerKey();
                machine.GetMachineKey();
                player.GetChoosedPlayerGameItem();
                machine.GetChoosedMachineGameItem();
            }
        }

        public void GameHelp()
        {
            Console.WriteLine(Properties.Resources.gameRulesMessage);
            Console.WriteLine(Properties.Resources.playerWaitForInputMessage);
            player.GetPlayerKey();
            player.GetChoosedPlayerMenu();
            GameMenuNavigation();
        }

        public void GameExit()
        {
            Environment.Exit(0);
        }
    }
}
