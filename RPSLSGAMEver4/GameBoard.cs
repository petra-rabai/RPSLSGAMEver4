using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSLSGAMEver4
{
    public class GameBoard
    {
        readonly Player player = new Player();
        readonly Machine machine = new Machine();

        private string gameResultTimeStamp = DateTime.Now.ToString("\n MM/dd/yyyy h:mm tt\n");

        private bool gameDirectoryExists;

        private string gameResult = "";

        private string gameResultDirectory = Properties.Settings.Default.FolderPath;

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

        private Tuple<string, string> gameCompareChoosedItems;

        private Dictionary<Tuple<string, string>, string> gameWinner = new Dictionary<Tuple<string, string>, string>();

        public Dictionary<char, string> GameMenu { get => gameMenu; set => gameMenu = value; }
        public Dictionary<char, string> GameItems { get => gameItems; set => gameItems = value; }
        public bool GameDirectoryExists { get => gameDirectoryExists; set => gameDirectoryExists = value; }
        public string GameResultDirectory { get => gameResultDirectory; set => gameResultDirectory = value; }
        public string GameResult { get => gameResult; set => gameResult = value; }
        public string GameResultTimeStamp { get => gameResultTimeStamp; set => gameResultTimeStamp = value; }
        public Tuple<string, string> GameCompareChoosedItems { get => gameCompareChoosedItems; set => gameCompareChoosedItems = value; }
        public Dictionary<Tuple<string, string>, string> GameWinner { get => gameWinner; set => gameWinner = value; }

        public void GameWelcomeScreenInitialize()
        {
            SetGameTitle();
            SetGameWelcomeMessage();
            SetPlayerWaitForInputMessage();
            SetPlayerKey();
            SetChoosedPlayerMenu();
            GameMenuNavigation();
        }

        public static void SetGameTitle()
        {
            Console.Title = Properties.Resources.gameTitle;
        }

        public static void SetGameWelcomeMessage()
        {
            Console.WriteLine(Properties.Resources.gameWelcomeMessage);
        }

        public void GameMenuNavigation()
        {
            SetChoosedPlayerMenu();
            switch (player.PlayerChoosedGameMenu)
            {
                case "Start the Game":
                    GameCore();
                    break;
                case "Game Help":
                    GameHelp();
                    break;
                case "Back to the Menu":
                    GameWelcomeScreenInitialize();
                    break;
                case "Save the Result":
                    GameSaveing();
                    break;
                case "Quit the Game":
                    GameExit();
                    break;
                default:
                    break;
            }
        }

        public void GameSaveing()
        {
            GameCheckSaveDirectoryExsits();
            SetPlayerName();
            SaveTheResultToFile();
        }

        public void SaveTheResultToFile()
        {
            GameResult = GameResultTimeStamp
                                             + Properties.Resources.playerNameMessage
                                             + player.PlayerName
                                             + GameWinner.Values
                                             + Properties.Resources.playerPointMessage
                                             + player.PlayerPoint
                                             + Properties.Resources.machinePointMessage
                                             + machine.MachinePoint
                                             + Properties.Resources.playerChoosedOptionMessage
                                             + player.PlayerChoosedGameItem
                                             + Properties.Resources.machineChoosedOtionMessage
                                             + machine.MachineChoosedGameItem;
            gameResultFullPath = GameResultDirectory + Properties.Resources.gameSavedDataFileName;
            File.AppendAllText(gameResultFullPath, GameResult);
        }

        public void SetPlayerName()
        {
            player.GetPlayerName();
        }

        public void GameCheckSaveDirectoryExsits()
        {
            GameDirectoryExists = Directory.Exists(GameResultDirectory);
            if (!GameDirectoryExists)
            {
                Directory.CreateDirectory(GameResultDirectory);
            }
        }

        public void GameCore()
        {
            SetGameAvailableItems();
            SetPlayerPoint();
            SetMachinePoint();
            SetPlayerKey();
            SetMachineKey();
            SetPlayerGameItem();
            SetMachineGameItem();
            GameItemsEqualityCheck();
            SetGameCompareItems();
            GameRulesCheck(GameCompareChoosedItems.Item1, GameCompareChoosedItems.Item2);
            GameShowTheResult();
            GameFinalize();
        }

        public void GameFinalize()
        {
            SetGameFinalizeMenuNavigationMessage();
            SetPlayerWaitForInputMessage();
            SetPlayerKey();
            GameMenuNavigation();
        }

        public void SetGameFinalizeMenuNavigationMessage()
        {
            Console.WriteLine(Properties.Resources.playerGameFinalizeNavigationMessage);
        }

        public void GameShowTheResult()
        {
            if (player.PlayerPoint > machine.MachinePoint)
            {
                SetPlayerWinMessage();
            }
            else
            {
                SetMachineWinMessage();
            }
        }

        public void SetMachineWinMessage()
        {
            Console.WriteLine(Properties.Resources.playerLoseMessage
                                              + gameWinner[GameCompareChoosedItems]
                                              + Properties.Resources.machinePointMessage
                                              + machine.MachinePoint
                                              + Properties.Resources.playerChoosedOptionMessage
                                              + player.PlayerChoosedGameItem
                                              + Properties.Resources.machineChoosedOtionMessage
                                              + machine.MachineChoosedGameItem);
        }

        public void SetPlayerWinMessage()
        {
            Console.WriteLine(Properties.Resources.playerWinMessage
                                              + gameWinner[GameCompareChoosedItems]
                                              + Properties.Resources.playerPointMessage
                                              + player.PlayerPoint
                                              + Properties.Resources.playerChoosedOptionMessage
                                              + player.PlayerChoosedGameItem
                                              + Properties.Resources.machineChoosedOtionMessage
                                              + machine.MachineChoosedGameItem);
        }

        public void GameRulesCheck(string optionOne, string optionTwo)
        {
            
            if (optionOne == GameCompareChoosedItems.Item1 && optionTwo == GameCompareChoosedItems.Item2)
            {
                gameWinner.Add(GameCompareChoosedItems, optionOne);
                player.PlayerPoint++;
            }
            else if (optionOne == GameCompareChoosedItems.Item2 && optionTwo == GameCompareChoosedItems.Item1)
            {
                gameWinner.Add(GameCompareChoosedItems, optionOne);
                machine.MachinePoint++;
            }
        }

        public void SetGameCompareItems()
        {
            gameCompareChoosedItems = new Tuple<string, string>(player.PlayerChoosedGameItem, machine.MachineChoosedGameItem);
        }

        public static void SetGameAvailableItems()
        {
            Console.WriteLine(Properties.Resources.gameAvailableItems);
        }

        public void SetMachineGameItem()
        {
            machine.GetChoosedMachineGameItem();
        }

        public void SetPlayerGameItem()
        {
            player.GetChoosedPlayerGameItem();
        }

        public void SetMachineKey()
        {
            machine.GetMachineKey();
        }

        public void SetPlayerKey()
        {
            player.GetPlayerKey();
        }

        public void SetMachinePoint()
        {
            machine.GetMachinePoint();
        }

        public void SetPlayerPoint()
        {
            player.GetPlayerPoint();
        }

        public void GameItemsEqualityCheck()
        {
            if (player.PlayerChoosedGameItem == machine.MachineChoosedGameItem)
            {
                SetGameItemsEqualMessage();
                SetPlayerInvalidAction();
                SetPlayerKey();
                SetMachineKey();
                SetPlayerGameItem();
                SetMachineGameItem();
            }
        }

        public static void SetGameItemsEqualMessage()
        {
            Console.WriteLine(Properties.Resources.gameItemsEqualMessage);
        }

        public void SetPlayerInvalidAction()
        {
            player.NotifyPalyerToAnInvalidAction();
        }

        public void GameHelp()
        {
            SetGameRulesMessage();
            SetPlayerWaitForInputMessage();
            SetPlayerKey();
            SetChoosedPlayerMenu();
            GameMenuNavigation();
        }

        public static void SetGameRulesMessage()
        {
            Console.WriteLine(Properties.Resources.gameRulesMessage);
        }

        public static void SetPlayerWaitForInputMessage()
        {
            Console.WriteLine(Properties.Resources.playerWaitForInputMessage);
        }

        public void SetChoosedPlayerMenu()
        {
            player.GetChoosedPlayerMenu();
        }

        public void GameExit()
        {
            Environment.Exit(0);
        }
    }
}
