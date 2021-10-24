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
        public string winner;
        public string GameResultTimeStamp { get; set; } = DateTime.Now.ToString("\n MM/dd/yyyy h:mm tt\n");
        public Tuple<string, string> GameCompareChoosedItems { get; set; }
        public Dictionary<Tuple<string, string>, string> GameWinner { get; set; } = new Dictionary<Tuple<string, string>, string>();

        public string gameResultFullPath = "";

        public void GameInitialize(Player player, Machine machine, GameBoard game)
        {
            GameWelcomeScreenInitialize();
            PlayerSelectGameMenuItem(player, machine, game);
        }

        public void GameWelcomeScreenInitialize()
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
                    Game(player, machine, game);
                    break;
                case "Game Help":
                    GameHelp(player,machine,game);
                    break;
                case "Back to the Menu":
                    GameInitialize(player,machine,game);
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

        public void Game(Player player, Machine machine, GameBoard game)
        {
            GameStart(player, machine, game);
            GameCore(player, machine, game);
            GameFinalize(player, machine, game);
        }

        public void GameStart(Player player, Machine machine, GameBoard game)
        {
            PointsReset(player, machine);
            WriteGameAvailableItems();
            WritePlayerWaitForInputMessage();
        }

        public void GameCore(Player player,Machine machine,GameBoard game)
        {
            PlayerSelectGameItem(player, game);
            MachineSelectGameItem(machine, game);
            
            GameItemsEqualityCheck(player, machine, game);

            SetGameCompareItems(player, machine);
            RuleValidator(GameCompareChoosedItems.Item1, GameCompareChoosedItems.Item2);
            GetTheGameWinner(player, machine, GameCompareChoosedItems.Item1, GameCompareChoosedItems.Item2);

            GameShowTheResult(player, machine);
        }

        public void MachineSelectGameItem(Machine machine, GameBoard game)
        {
            SetMachineKey(machine, game);
            SetMachineGameItem(machine, game);
        }

        public void PlayerSelectGameItem(Player player, GameBoard game)
        {
            SetPlayerKey(player, game);
            SetPlayerGameItem(player, game);
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
            machine.GetChoosedMachineGameItem(game);
        }

        public void GameItemsEqualityCheck(Player player, Machine machine, GameBoard game)
        {
            if (player.PlayerChoosedGameItem == machine.MachineChoosedGameItem)
            {
                WriteGameItemsEqualMessage();
                SetPlayerInvalidAction(player, game);
                PlayerSelectGameItem(player, game);
                MachineSelectGameItem(machine, game);
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

        public string RuleValidator(string optionOne, string optionTwo)
        {
            ValidateScissorVsPaper(optionOne, optionTwo);
            ValidateRockVsScissor(optionOne, optionTwo);
            ValidateRockVsPaper(optionOne, optionTwo);
            ValidateRockVsLizard(optionOne, optionTwo);
            ValidateLizardVsSpock(optionOne, optionTwo);
            ValidateSpockVsScissor(optionOne, optionTwo);
            ValidateScissorVsLizard(optionOne, optionTwo);
            ValidatePaperVsLizard(optionOne, optionTwo);
            ValidateSpockVsPaper(optionOne, optionTwo);
            ValidateRockVsSpock(optionOne, optionTwo);

            return winner;
        }

        public void ValidateRockVsSpock(string optionOne, string optionTwo)
        {
            if (optionOne == "Rock" && optionTwo == "Spock")
            {
                winner = "Spock";
            }
            if (optionOne == "Spock" && optionTwo == "Rock")
            {
                winner = "Spock";
            }
        }

        public void ValidateSpockVsPaper(string optionOne, string optionTwo)
        {
            if (optionOne == "Spock" && optionTwo == "Paper")
            {
                winner = "Paper";
            }
            if (optionOne == "Paper" && optionTwo == "Spock")
            {
                winner = "Paper";
            }
        }

        public void ValidatePaperVsLizard(string optionOne, string optionTwo)
        {
            if (optionOne == "Lizard" && optionTwo == "Paper")
            {
                winner = "Lizard";
            }
            if (optionOne == "Paper" && optionTwo == "Lizard")
            {
                winner = "Lizard";
            }
        }

        public void ValidateScissorVsLizard(string optionOne, string optionTwo)
        {
            if (optionOne == "Scissor" && optionTwo == "Lizard")
            {
                winner = "Scissor";
            }
            if (optionOne == "Lizard" && optionTwo == "Scissor")
            {
                winner = "Scissor";
            }
        }

        public void ValidateSpockVsScissor(string optionOne, string optionTwo)
        {
            if (optionOne == "Spock" && optionTwo == "Scissor")
            {
                winner = "Spock";
            }
            if (optionOne == "Scissor" && optionTwo == "Spock")
            {
                winner = "Spock";
            }
        }

        public void ValidateLizardVsSpock(string optionOne, string optionTwo)
        {
            if (optionOne == "Lizard" && optionTwo == "Spock")
            {
                winner = "Lizard";
            }
            if (optionOne == "Spock" && optionTwo == "Lizard")
            {
                winner = "Lizard";
            }
        }

        public void ValidateRockVsLizard(string optionOne, string optionTwo)
        {
            if (optionOne == "Rock" && optionTwo == "Lizard")
            {
                winner = "Rock";
            }
            if (optionOne == "Lizard" && optionTwo == "Rock")
            {
                winner = "Rock";
            }
        }

        public void ValidateRockVsPaper(string optionOne, string optionTwo)
        {
            if (optionOne == "Rock" && optionTwo == "Paper")
            {
                winner = "Paper";
            }
            if (optionOne == "Paper" && optionTwo == "Rock")
            {
                winner = "Paper";
            }
        }

        public void ValidateRockVsScissor(string optionOne, string optionTwo)
        {
            if (optionOne == "Rock" && optionTwo == "Scissor")
            {
                winner = "Rock";
            }
            if (optionOne == "Scissor" && optionTwo == "Rock")
            {
                winner = "Rock";
            }
        }

        public void ValidateScissorVsPaper(string optionOne, string optionTwo)
        {
            if (optionOne == "Scissor" && optionTwo == "Paper")
            {
                winner = "Scissor";
            }
            if (optionOne == "Paper" && optionTwo == "Scissor")
            {
                winner = "Scissor";
            }
        }

        public void GetTheGameWinner(Player player, Machine machine, string optionOne, string optionTwo)
        {
            if (optionOne == winner)
            {
                GameWinner.Add(GameCompareChoosedItems, optionOne);
                player.PlayerPoint++;
            }
            if (optionTwo == winner)
            {
                GameWinner.Add(GameCompareChoosedItems, optionTwo);
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
            WriteHelpMenuNavigationMessage();
            WritePlayerWaitForInputMessage();
            PlayerSelectGameMenuItem(player, machine, game);
        }

        public void WriteHelpMenuNavigationMessage()
        {
            Console.WriteLine(Properties.Resources.playerGameRulesNavigationMessage);
        }

        public void SetGameRulesMessage()
        {
            Console.WriteLine(Properties.Resources.gameRulesMessage);
        }

        public void GameFinalize(Player player, Machine machine, GameBoard game)
        {
            WriteGameFinalizeMenuNavigationMessage();
            WritePlayerWaitForInputMessage();
            PlayerSelectGameMenuItem(player, machine, game);
        }

        public void PlayerSelectGameMenuItem(Player player, Machine machine, GameBoard game)
        {
            SetPlayerKey(player, game);
            SetChoosedPlayerMenu(player, game);
            GameMenuNavigation(player, machine, game);
        }

        public void GameExit()
        {
            Environment.Exit(0);
        }
    }
}
