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
        public Dictionary<char, string> GameMenu { get => gameMenu; set => gameMenu = value; }
        public Dictionary<char, string> GameItems { get => gameItems; set => gameItems = value; }
        public bool GameDirectoryExists { get => gameDirectoryExists; set => gameDirectoryExists = value; }
        public string GameResultDirectory { get => gameResultDirectory; set => gameResultDirectory = value; }
        public string GameResult { get => gameResult; set => gameResult = value; }
        public string GameResultTimeStamp { get => gameResultTimeStamp; set => gameResultTimeStamp = value; }
        public Tuple<string, string> GameCompareChoosedItems { get => gameCompareChoosedItems; set => gameCompareChoosedItems = value; }
        public Dictionary<Tuple<string, string>, string> GameWinner { get => gameWinner; set => gameWinner = value; }

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

       

        public void GameWelcomeScreenInitialize(Player player, Machine machine, GameBoard game)
        {
            SetGameTitle();
            SetGameWelcomeMessage();
            SetPlayerWaitForInputMessage();
            SetPlayerKey(player,game);
            SetChoosedPlayerMenu(player,game);
            GameMenuNavigation(player,machine,game);
        }

        public void SetGameTitle()
        {
            Console.Title = Properties.Resources.gameTitle;
        }

        public void SetGameWelcomeMessage()
        {
            Console.WriteLine(Properties.Resources.gameWelcomeMessage);
        }

        public void GameMenuNavigation(Player player, Machine machine, GameBoard game)
        {
            SetChoosedPlayerMenu(player,game);
            switch (player.PlayerChoosedGameMenu)
            {
                case "Start the Game":
                    GameCore(player,machine,game);
                    break;
                case "Game Help":
                    GameHelp(player,machine,game);
                    break;
                case "Back to the Menu":
                    GameWelcomeScreenInitialize(player,machine,game);
                    break;
                case "Save the Result":
                    GameSaveing(player,machine);
                    break;
                case "Quit the Game":
                    GameExit();
                    break;
                default:
                    break;
            }
        }

        public void GameSaveing(Player player, Machine machine)
        {
            GameCheckSaveDirectoryExsits();
            SetPlayerName(player);
            SaveTheResultToFile(player,machine);
        }

        public void SaveTheResultToFile(Player player, Machine machine)
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

        public void SetPlayerName(Player player)
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

        public void GameCore(Player player, Machine machine, GameBoard game)
        {
            SetGameAvailableItems();
            SetPlayerPoint(player);
            SetMachinePoint(machine);
            SetPlayerKey(player,game);
            SetMachineKey(machine,game);
            SetPlayerGameItem(player,game);
            SetMachineGameItem(machine,game);
            GameItemsEqualityCheck(player,machine,game);
            SetGameCompareItems(player,machine);
            GameRulesCheck(player,machine,GameCompareChoosedItems.Item1, GameCompareChoosedItems.Item2);
            GameShowTheResult(player,machine);
            GameFinalize(player,machine,game);
        }

        public void GameFinalize(Player player,Machine machine, GameBoard game)
        {
            SetGameFinalizeMenuNavigationMessage();
            SetPlayerWaitForInputMessage();
            SetPlayerKey(player,game);
            GameMenuNavigation(player, machine,game);
        }

        public void SetGameFinalizeMenuNavigationMessage()
        {
            Console.WriteLine(Properties.Resources.playerGameFinalizeNavigationMessage);
        }

        public void GameShowTheResult(Player player, Machine machine)
        {
            if (player.PlayerPoint > machine.MachinePoint)
            {
                SetPlayerWinMessage(player,machine);
            }
            else
            {
                SetMachineWinMessage(player,machine);
            }
        }

        public void SetMachineWinMessage(Player player, Machine machine)
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

        public void SetPlayerWinMessage(Player player, Machine machine)
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

        public void GameRulesCheck(Player player, Machine machine, string optionOne, string optionTwo)
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

        public void SetGameCompareItems(Player player, Machine machine)
        {
            gameCompareChoosedItems = new Tuple<string, string>(player.PlayerChoosedGameItem, machine.MachineChoosedGameItem);
        }

        public void SetGameAvailableItems()
        {
            Console.WriteLine(Properties.Resources.gameAvailableItems);
        }

        public void SetMachineGameItem(Machine machine, GameBoard game)
        {
            machine.GetChoosedMachineGameItem(game);
        }

        public void SetPlayerGameItem(Player player, GameBoard game)
        {
            player.GetChoosedPlayerGameItem(game);
        }

        public void SetMachineKey(Machine machine, GameBoard game)
        {
            machine.GetMachineKey(game);
        }

        public void SetPlayerKey(Player player, GameBoard game)
        {
            player.GetPlayerKey(game);
        }

        public void SetMachinePoint(Machine machine)
        {
            machine.GetMachinePoint();
        }

        public void SetPlayerPoint(Player player)
        {
            player.GetPlayerPoint();
        }

        public void GameItemsEqualityCheck(Player player, Machine machine, GameBoard game)
        {
            if (player.PlayerChoosedGameItem == machine.MachineChoosedGameItem)
            {
                SetGameItemsEqualMessage();
                SetPlayerInvalidAction(player,game);
                SetPlayerKey(player,game);
                SetMachineKey(machine,game);
                SetPlayerGameItem(player,game);
                SetMachineGameItem(machine,game);
            }
        }

        public void SetGameItemsEqualMessage()
        {
            Console.WriteLine(Properties.Resources.gameItemsEqualMessage);
        }

        public void SetPlayerInvalidAction(Player player, GameBoard game)
        {
            player.NotifyPalyerToAnInvalidAction(game);
        }

        public void GameHelp(Player player,Machine machine, GameBoard game)
        {
            SetGameRulesMessage();
            SetPlayerWaitForInputMessage();
            SetPlayerKey(player,game);
            SetChoosedPlayerMenu(player,game);
            GameMenuNavigation(player,machine,game);
        }

        public void SetGameRulesMessage()
        {
            Console.WriteLine(Properties.Resources.gameRulesMessage);
        }

        public void SetPlayerWaitForInputMessage()
        {
            Console.WriteLine(Properties.Resources.playerWaitForInputMessage);
        }

        public void SetChoosedPlayerMenu(Player player, GameBoard game)
        {
            player.GetChoosedPlayerMenu(game);
        }

        public void GameExit()
        {
            Environment.Exit(0);
        }
    }
}
