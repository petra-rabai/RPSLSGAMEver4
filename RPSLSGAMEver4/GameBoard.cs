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
        }
        
        public void GameMenuNavigation()
        {
            player.GetChoosedPlayerMenu();
            switch (player.PlayerChoosedGameMenu)
            {
                case "Start the Game":
                    //
                    break;
                case "Game Help":
                    //
                    break;
                case "Back to the Menu":
                    //
                    break;
                case "Save the Result":
                    //
                    break;
                case "Quit the Game":
                    //
                    break;
                default:
                    break;
            }
        }
    }
}
