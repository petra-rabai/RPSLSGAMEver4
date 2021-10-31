namespace RPSLSGAMEver4
{
    public interface IPlayer
    {
        string PlayerName { get; set; }
        string PlayerChoosedGameMenu { get; set; }
        char PlayerPressedkey { get; set; }
        string PlayerChoosedGameItem { get; set; }
        int PlayerPoint { get; set; }
        char GetPlayerKey(Board game);
        void WritePlayerWaitForMessage();
        int GetPlayerPoint();
        string GetPlayerName();
        void ReadPlayerNameFromTheConsole();
        void WritePlayerNameMessage();
        void ReadKeyboard();
        void NotifyPalyerToAnInvalidAction(Board game);
        string GetChoosedPlayerMenu(Board game);
        string GetChoosedPlayerGameItem(Board game);
    }
}