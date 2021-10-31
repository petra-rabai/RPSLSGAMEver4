using System;
using System.Collections.Generic;

namespace RPSLSGAMEver4
{
    public class Player : IPlayer
    {
        public string PlayerName { get; set; }
        public string PlayerChoosedGameMenu { get; set; }
        public char PlayerPressedkey { get; set; }
        public string PlayerChoosedGameItem { get; set; }
        public int PlayerPoint { get; set; }

        public char GetPlayerKey(Board game)
        {
            ReadKeyboard();
            while ((!game.GameMenu.ContainsKey(PlayerPressedkey)) && (!game.GameItems.ContainsKey(PlayerPressedkey)))
            {
                NotifyPalyerToAnInvalidAction(game);
                ReadKeyboard();
            }

            return PlayerPressedkey;
        }

        public void WritePlayerWaitForMessage()
        {
            Console.WriteLine(Properties.Resources.playerWaitForInputMessage);
        }

        public int GetPlayerPoint()
        {
            PlayerPoint = 0;
            return PlayerPoint;
        }

        public string GetPlayerName()
        {
            WritePlayerNameMessage();
            WritePlayerWaitForMessage();
            ReadPlayerNameFromTheConsole();

            return PlayerName;
        }

        public void ReadPlayerNameFromTheConsole()
        {
            PlayerName = Console.ReadLine();
        }

        public void WritePlayerNameMessage()
        {
            Console.WriteLine(Properties.Resources.playerAddNameMessage);
        }

        public void ReadKeyboard()
        {
            ConsoleKeyInfo hitkey = Console.ReadKey();
            PlayerPressedkey = Char.Parse(hitkey.Key.ToString());
        }

        public void NotifyPalyerToAnInvalidAction(Board game)
        {
            Console.WriteLine(Properties.Resources.playerHitValidKeyMessage);

            foreach (KeyValuePair<char, string> gameMenupair in game.GameMenu)
            {
                Console.WriteLine(gameMenupair.Key + " - " + gameMenupair.Value + "\n");
            }
            foreach (KeyValuePair<char, string> gameItempair in game.GameItems)
            {
                Console.WriteLine(gameItempair.Key + " - " + gameItempair.Value + "\n");
            }

            WritePlayerWaitForMessage();
        }

        public string GetChoosedPlayerMenu(Board game)
        {
            PlayerChoosedGameMenu = game.GameMenu[PlayerPressedkey];
            return PlayerChoosedGameMenu;
        }

        public string GetChoosedPlayerGameItem(Board game)
        {
            PlayerChoosedGameItem = game.GameItems[PlayerPressedkey];
            return PlayerChoosedGameItem;
        }
    }
}
