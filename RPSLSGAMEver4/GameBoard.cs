using System;
using System.Collections.Generic;
using System.IO;
using RPSLSGAMEver4.Properties;

namespace RPSLSGAMEver4
{
    public class Board : IBoard
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

        public Dictionary<Tuple<string, string>, string> RuleCheck { get; } =
            new Dictionary<Tuple<string, string>, string>()
            {
                {new Tuple<string, string>("Paper", "Scissor"), "Scissor"},
                {new Tuple<string, string>("Scissor", "Paper"), "Scissor"},
                {new Tuple<string, string>("Rock", "Scissor"), "Rock"},
                {new Tuple<string, string>("Scissor", "Rock"), "Rock"},
                {new Tuple<string, string>("Rock", "Lizard"), "Rock"},
                {new Tuple<string, string>("Lizard", "Rock"), "Rock"},
                {new Tuple<string, string>("Lizard", "Spock"), "Lizard"},
                {new Tuple<string, string>("Spock", "Lizard"), "Lizard"},
                {new Tuple<string, string>("Spock", "Scissor"), "Spock"},
                {new Tuple<string, string>("Scissor", "Spock"), "Spock"},
                {new Tuple<string, string>("Scissor", "Lizard"), "Scissor"},
                {new Tuple<string, string>("Lizard", "Scissor"), "Scissor"},
                {new Tuple<string, string>("Paper", "Lizard"), "Lizard"},
                {new Tuple<string, string>("Lizard", "Paper"), "Lizard"},
                {new Tuple<string, string>("Paper", "Spock"), "Paper"},
                {new Tuple<string, string>("Spock", "Paper"), "Paper"},
                {new Tuple<string, string>("Rock", "Spock"), "Spock"},
                {new Tuple<string, string>("Spock", "Rock"), "Spock"},
                {new Tuple<string, string>("Rock", "Paper"), "Paper"},
                {new Tuple<string, string>("Paper", "Rock"), "Paper"}
            };

        public bool GameDirectoryExists { get; set; }
        public string GameResultDirectory { get; set; } = Settings.Default.FolderPath;
        public string GameResult { get; set; } = "";
        public string GameResultTimeStamp { get; set; } = DateTime.Now.ToString("\n MM/dd/yyyy h:mm tt\n");
        public Tuple<string, string> GameCompareChoosedItems { get; set; }
        public Dictionary<Tuple<string, string>, string> GameWinner { get; set; } = new Dictionary<Tuple<string, string>, string>();
        public string Winner { get; set; }

        public string ResultFullPath = "";

        public void Initialize(Player player, Machine machine, Board game)
        {
            WelcomeScreenInitialize();
            PlayerSelectGameMenuItem(player, machine, game);
        }

        public void WelcomeScreenInitialize()
        {
            SetGameTitle();
            WriteGameWelcomeMessage();
            WritePlayerWaitForInputMessage();
        }

        public void SetGameTitle()
        {
            Console.Title = Resources.gameTitle;
        }

        public void WriteGameWelcomeMessage()
        {
            Console.WriteLine(Resources.gameWelcomeMessage);
        }

        public void WritePlayerWaitForInputMessage()
        {
            Console.WriteLine(Resources.playerWaitForInputMessage);
        }

        public void SetPlayerKey(Player player, Board game)
        {
            player.GetPlayerKey(game);
        }

        public void SetChoosedPlayerMenu(Player player, Board game)
        {
            player.GetChoosedPlayerMenu(game);
        }

        public void MenuNavigation(Player player, Machine machine, Board game)
        {
            SetChoosedPlayerMenu(player, game);
            switch (player.PlayerChoosedGameMenu)
            {
                case "Start the Game":
                    Game(player, machine, game);
                    break;
                case "Game Help":
                    Help(player, machine, game);
                    break;
                case "Back to the Menu":
                    Initialize(player, machine, game);
                    break;
                case "Save the Result":
                    Saveing(player, machine);
                    break;
                case "Quit the Game":
                    Exit();
                    break;
            }
        }

        public void Game(Player player, Machine machine, Board game)
        {
            Start(player, machine, game);
            Core(player, machine, game);
            Finalize(player, machine, game);
        }

        public void Start(Player player, Machine machine, Board game)
        {
            PointsReset(player, machine);
            WriteGameAvailableItems();
            WritePlayerWaitForInputMessage();
        }

        public void Core(Player player, Machine machine, Board game)
        {
            PlayerSelectGameItem(player, game);
            MachineSelectGameItem(machine, game);

            ItemsEqualityCheck(player, machine, game);

            SetGameCompareItems(player, machine);
            RuleValidator(GameCompareChoosedItems.Item1, GameCompareChoosedItems.Item2);
            GetTheGameWinner(player, machine, GameCompareChoosedItems.Item1, GameCompareChoosedItems.Item2);

            ShowTheResult(player, machine);
        }

        public void MachineSelectGameItem(Machine machine, Board game)
        {
            SetMachineKey(machine, game);
            SetMachineGameItem(machine, game);
        }

        public void PlayerSelectGameItem(Player player, Board game)
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
            Console.WriteLine(Resources.gameAvailableItems);
        }

        public void SetPlayerPoint(Player player)
        {
            player.GetPlayerPoint();
        }

        public void SetMachinePoint(Machine machine)
        {
            machine.GetMachinePoint();
        }

        public void SetMachineKey(Machine machine, Board game)
        {
            machine.GetMachineKey(game);
        }

        public void SetPlayerGameItem(Player player, Board game)
        {
            player.GetChoosedPlayerGameItem(game);
        }

        public void SetMachineGameItem(Machine machine, Board game)
        {
            machine.GetChoosedMachineGameItem(game);
        }

        public void ItemsEqualityCheck(Player player, Machine machine, Board game)
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
            Console.WriteLine(Resources.gameItemsEqualMessage);
        }

        public void SetPlayerInvalidAction(Player player, Board game)
        {
            player.NotifyPalyerToAnInvalidAction(game);
        }

        public void SetGameCompareItems(Player player, Machine machine)
        {
            GameCompareChoosedItems = new Tuple<string, string>(player.PlayerChoosedGameItem, machine.MachineChoosedGameItem);
        }

        public string RuleValidator(string optionOne, string optionTwo)
        {
            if (RuleCheck.ContainsKey(GameCompareChoosedItems))
            {
                Winner = RuleCheck[GameCompareChoosedItems];
            }

            return Winner;
        }

        public void GetTheGameWinner(Player player, Machine machine, string optionOne, string optionTwo)
        {
            if (optionOne == Winner)
            {
                GameWinner.Add(GameCompareChoosedItems, optionOne);
                player.PlayerPoint++;
            }
            if (optionTwo == Winner)
            {
                GameWinner.Add(GameCompareChoosedItems, optionTwo);
                machine.MachinePoint++;
            }
        }

        public void ShowTheResult(Player player, Machine machine)
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
            Console.WriteLine(Resources.playerWinMessage
                                              + GameWinner[GameCompareChoosedItems]
                                              + Resources.playerPointMessage
                                              + player.PlayerPoint
                                              + Resources.playerChoosedOptionMessage
                                              + player.PlayerChoosedGameItem
                                              + Resources.machineChoosedOtionMessage
                                              + machine.MachineChoosedGameItem);
        }

        public void WriteMachineWinMessage(Player player, Machine machine)
        {
            Console.WriteLine(Resources.playerLoseMessage
                                              + GameWinner[GameCompareChoosedItems]
                                              + Resources.machinePointMessage
                                              + machine.MachinePoint
                                              + Resources.playerChoosedOptionMessage
                                              + player.PlayerChoosedGameItem
                                              + Resources.machineChoosedOtionMessage
                                              + machine.MachineChoosedGameItem);
        }

        public void WriteGameFinalizeMenuNavigationMessage()
        {
            Console.WriteLine(Resources.playerGameFinalizeNavigationMessage);
        }

        public void Saveing(Player player, Machine machine)
        {
            SetPlayerName(player);
            SaveTheResultToFile(player, machine);
        }

        public void CheckSaveDirectoryExsits()
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
            CheckSaveDirectoryExsits();
            GameResult = GameResultTimeStamp
                                             + Resources.playerNameMessage
                                             + player.PlayerName
                                             + GameWinner.Values
                                             + Resources.playerPointMessage
                                             + player.PlayerPoint
                                             + Resources.machinePointMessage
                                             + machine.MachinePoint
                                             + Resources.playerChoosedOptionMessage
                                             + player.PlayerChoosedGameItem
                                             + Resources.machineChoosedOtionMessage
                                             + machine.MachineChoosedGameItem;
            ResultFullPath = GameResultDirectory + Resources.gameSavedDataFileName;
            File.AppendAllText(ResultFullPath, GameResult);
        }

        public void Help(Player player, Machine machine, Board game)
        {
            SetGameRulesMessage();
            WriteHelpMenuNavigationMessage();
            WritePlayerWaitForInputMessage();
            PlayerSelectGameMenuItem(player, machine, game);
        }

        public void WriteHelpMenuNavigationMessage()
        {
            Console.WriteLine(Resources.playerGameRulesNavigationMessage);
        }

        public void SetGameRulesMessage()
        {
            Console.WriteLine(Resources.gameRulesMessage);
        }

        public void Finalize(Player player, Machine machine, Board game)
        {
            WriteGameFinalizeMenuNavigationMessage();
            WritePlayerWaitForInputMessage();
            PlayerSelectGameMenuItem(player, machine, game);
        }

        public void PlayerSelectGameMenuItem(Player player, Machine machine, Board game)
        {
            SetPlayerKey(player, game);
            SetChoosedPlayerMenu(player, game);
            MenuNavigation(player, machine, game);
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
