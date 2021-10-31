using System;
using System.Collections.Generic;

namespace RPSLSGAMEver4
{
    public interface IBoard
    {
        Dictionary<char, string> GameMenu { get; set; }
        Dictionary<char, string> GameItems { get; set; }
        Dictionary<Tuple<string, string>, string> RuleCheck { get; }
        bool GameDirectoryExists { get; set; }
        string GameResultDirectory { get; set; }
        string GameResult { get; set; }
        string GameResultTimeStamp { get; set; }
        Tuple<string, string> GameCompareChoosedItems { get; set; }
        Dictionary<Tuple<string, string>, string> GameWinner { get; set; }
        string Winner { get; set; }
        void Initialize(Player player, Machine machine, Board game);
        void WelcomeScreenInitialize();
        void SetGameTitle();
        void WriteGameWelcomeMessage();
        void WritePlayerWaitForInputMessage();
        void SetPlayerKey(Player player, Board game);
        void SetChoosedPlayerMenu(Player player, Board game);
        void MenuNavigation(Player player, Machine machine, Board game);
        void Game(Player player, Machine machine, Board game);
        void Start(Player player, Machine machine, Board game);
        void Core(Player player, Machine machine, Board game);
        void MachineSelectGameItem(Machine machine, Board game);
        void PlayerSelectGameItem(Player player, Board game);
        void PointsReset(Player player, Machine machine);
        void WriteGameAvailableItems();
        void SetPlayerPoint(Player player);
        void SetMachinePoint(Machine machine);
        void SetMachineKey(Machine machine, Board game);
        void SetPlayerGameItem(Player player, Board game);
        void SetMachineGameItem(Machine machine, Board game);
        void ItemsEqualityCheck(Player player, Machine machine, Board game);
        void WriteGameItemsEqualMessage();
        void SetPlayerInvalidAction(Player player, Board game);
        void SetGameCompareItems(Player player, Machine machine);
        string RuleValidator(string optionOne, string optionTwo);
        void GetTheGameWinner(Player player, Machine machine, string optionOne, string optionTwo);
        void ShowTheResult(Player player, Machine machine);
        void WritePlayerWinMessage(Player player, Machine machine);
        void WriteMachineWinMessage(Player player, Machine machine);
        void WriteGameFinalizeMenuNavigationMessage();
        void Saveing(Player player, Machine machine);
        void CheckSaveDirectoryExsits();
        void SetPlayerName(Player player);
        void SaveTheResultToFile(Player player, Machine machine);
        void Help(Player player, Machine machine, Board game);
        void WriteHelpMenuNavigationMessage();
        void SetGameRulesMessage();
        void Finalize(Player player, Machine machine, Board game);
        void PlayerSelectGameMenuItem(Player player, Machine machine, Board game);
        void Exit();
    }
}