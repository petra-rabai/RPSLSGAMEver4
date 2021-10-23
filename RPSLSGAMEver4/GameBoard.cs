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
        public Dictionary<char, string> GameMenu { get; set; } = new Dictionary<char, string>
        {
            ['E'] = "Start the Game",
            ['H'] = "Game Help",
            ['B'] = "Back to the Menu",
            ['C'] = "Save the Result",
            ['Q'] = "Quit the Game"
        };
        public Dictionary<char, string> GameItems { get; set; } = new Dictionary<char, string>
        {
            ['P'] = "Paper",
            ['S'] = "Scissor",
            ['V'] = "Spock",
            ['R'] = "Rock",
            ['L'] = "Lizard"

        };
        public bool GameDirectoryExists { get; set; }
        public string GameResultDirectory { get; set; } = Properties.Settings.Default.FolderPath;
        public string GameResult { get; set; } = "";
        public string GameResultTimeStamp { get; set; } = DateTime.Now.ToString("\n MM/dd/yyyy h:mm tt\n");
        public Tuple<string, string> GameCompareChoosedItems { get; set; }
        public Dictionary<Tuple<string, string>, string> GameWinner { get; set; } = new Dictionary<Tuple<string, string>, string>();

        public string gameResultFullPath = "";

        public void GameWelcomeScreenInitialize(Player player, Machine machine, GameBoard game)
        {
            SetGameTitle();
            WriteGameWelcomeMessage();
            WritePlayerWaitForInputMessage(); 
        }

        public void SetGameTitle()
        {
            Console.Title = Properties.Resources.gameTitle;
        }

        public void WriteGameWelcomeMessage()
        {
            Console.WriteLine(Properties.Resources.gameWelcomeMessage);
        }

        public void WritePlayerWaitForInputMessage()
        {
            Console.WriteLine(Properties.Resources.playerWaitForInputMessage);
        }

        public void SetPlayerKey(Player player, GameBoard game)
        {
            player.GetPlayerKey(game);
        }

        public void SetChoosedPlayerMenu(Player player, GameBoard game)
        {
            player.GetChoosedPlayerMenu(game);
        }

        public void GameMenuNavigation(Player player, Machine machine, GameBoard game)
        {
            SetChoosedPlayerMenu(player,game);
            switch (player.PlayerChoosedGameMenu)
            {
                case "Start the Game":
                    GameStart(player,machine,game);
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

        public void GameStart(Player player, Machine machine, GameBoard game)
        {
            PointsReset(player, machine);
            WriteGameAvailableItems();
            WritePlayerWaitForInputMessage();
        }

        public void PointsReset(Player player, Machine machine)
        {
            SetPlayerPoint(player);
            SetMachinePoint(machine);
        }

        public void WriteGameAvailableItems()
        {
            Console.WriteLine(Properties.Resources.gameAvailableItems);
        }

        public void SetPlayerPoint(Player player)
        {
            player.GetPlayerPoint();
        }

        public void SetMachinePoint(Machine machine)
        {
            machine.GetMachinePoint();
        }

        public void SetMachineKey(Machine machine, GameBoard game)
        {  
            machine.GetMachineKey(game);
        }

        public void SetPlayerGameItem(Player player, GameBoard game)
        {
            player.GetChoosedPlayerGameItem(game);
        }

        public void SetMachineGameItem(Machine machine, GameBoard game)
        {
            SetMachineKey(machine, game);
            machine.GetChoosedMachineGameItem(game);
        }

        public void GameItemsEqualityCheck(Player player, Machine machine, GameBoard game)
        {
            if (player.PlayerChoosedGameItem == machine.MachineChoosedGameItem)
            {
                WriteGameItemsEqualMessage();
                SetPlayerInvalidAction(player, game);
                SetPlayerGameItem(player, game);
                SetMachineGameItem(machine, game);
            }
        }

        public void WriteGameItemsEqualMessage()
        {
            Console.WriteLine(Properties.Resources.gameItemsEqualMessage);
        }

        public void SetPlayerInvalidAction(Player player, GameBoard game)
        {
            player.NotifyPalyerToAnInvalidAction(game);
        }

        public void SetGameCompareItems(Player player, Machine machine)
        {
            GameCompareChoosedItems = new Tuple<string, string>(player.PlayerChoosedGameItem, machine.MachineChoosedGameItem);
        }

        public void GameRulesCheck(Player player, Machine machine, string optionOne, string optionTwo)
        {

            if (optionOne == GameCompareChoosedItems.Item1 && optionTwo == GameCompareChoosedItems.Item2)
            {
                GameWinner.Add(GameCompareChoosedItems, optionOne);
                player.PlayerPoint++;
            }
            else if (optionOne == GameCompareChoosedItems.Item2 && optionTwo == GameCompareChoosedItems.Item1)
            {
                GameWinner.Add(GameCompareChoosedItems, optionOne);
                machine.MachinePoint++;
            }
        }

        public void GameShowTheResult(Player player, Machine machine)
        {
            if (player.PlayerPoint > machine.MachinePoint)
            {
                WritePlayerWinMessage(player, machine);
            }
            else
            {
                WriteMachineWinMessage(player, machine);
            }
        }

        public void WritePlayerWinMessage(Player player, Machine machine)
        {
            Console.WriteLine(Properties.Resources.playerWinMessage
                                              + GameWinner[GameCompareChoosedItems]
                                              + Properties.Resources.playerPointMessage
                                              + player.PlayerPoint
                                              + Properties.Resources.playerChoosedOptionMessage
                                              + player.PlayerChoosedGameItem
                                              + Properties.Resources.machineChoosedOtionMessage
                                              + machine.MachineChoosedGameItem);
        }

        public void WriteMachineWinMessage(Player player, Machine machine)
        {
            Console.WriteLine(Properties.Resources.playerLoseMessage
                                              + GameWinner[GameCompareChoosedItems]
                                              + Properties.Resources.machinePointMessage
                                              + machine.MachinePoint
                                              + Properties.Resources.playerChoosedOptionMessage
                                              + player.PlayerChoosedGameItem
                                              + Properties.Resources.machineChoosedOtionMessage
                                              + machine.MachineChoosedGameItem);
        }

        public void WriteGameFinalizeMenuNavigationMessage()
        {
            Console.WriteLine(Properties.Resources.playerGameFinalizeNavigationMessage);
        }

        public void GameSaveing(Player player, Machine machine)
        {
            SetPlayerName(player);
            SaveTheResultToFile(player,machine);
        }

        public void GameCheckSaveDirectoryExsits()
        {
            GameDirectoryExists = Directory.Exists(GameResultDirectory);
            if (!GameDirectoryExists)
            {
                Directory.CreateDirectory(GameResultDirectory);
            }
        }

        public void SetPlayerName(Player player)
        {
            player.GetPlayerName();
        }

        public void SaveTheResultToFile(Player player, Machine machine)
        {
            GameCheckSaveDirectoryExsits();
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

        public void GameHelp(Player player,Machine machine, GameBoard game)
        {
            SetGameRulesMessage();
            WritePlayerWaitForInputMessage();
            SetPlayerKey(player,game);
            SetChoosedPlayerMenu(player,game);
            GameMenuNavigation(player,machine,game);
        }

        public void SetGameRulesMessage()
        {
            Console.WriteLine(Properties.Resources.gameRulesMessage);
        }

        public void GameExit()
        {
            Environment.Exit(0);
        }
    }
}
