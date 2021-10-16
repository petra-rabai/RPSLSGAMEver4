using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSLSGAMEver4
{
    public class Player
    {
        public string PlayerName { get => playerName; set => playerName = value; }
        public string PlayerChoosedGameMenu { get => playerChoosedGameMenu; set => playerChoosedGameMenu = value; }
        public char PlayerPressedkey { get => playerPressedkey; set => playerPressedkey = value; }
        public string PlayerChoosedGameItem { get => playerChoosedGameItem; set => playerChoosedGameItem = value; }
        public int PlayerPoint { get => playerPoint; set => playerPoint = value; }

        private string playerName;
        private string playerChoosedGameMenu;
        private int playerPoint;
        private char playerPressedkey;
        private string playerChoosedGameItem;

        public char GetPlayerKey(GameBoard game)
        {
            SetPlayerWaitForMessage();
            ReadKeyboard();
            while ((!game.GameMenu.ContainsKey(PlayerPressedkey)) && (!game.GameItems.ContainsKey(PlayerPressedkey)))
            {
                NotifyPalyerToAnInvalidAction(game);
                ReadKeyboard();
            }

            return PlayerPressedkey;
        }

        public void SetPlayerWaitForMessage()
        {
            Console.WriteLine(Properties.Resources.playerWaitForInputMessage);
        }

        public int GetPlayerPoint()
        {
            playerPoint = 0;
            return PlayerPoint;
        }

        public string GetPlayerName()
        {
            SetPlayerNameMessage();
            SetPlayerWaitForMessage();
            ReadPlayerNameFromTheConsole();

            return PlayerName;
        }

        public void ReadPlayerNameFromTheConsole()
        {
            playerName = Console.ReadLine();
        }

        public void SetPlayerNameMessage()
        {
            Console.WriteLine(Properties.Resources.playerAddNameMessage);
        }

        public void ReadKeyboard()
        {
            ConsoleKeyInfo Hitkey = Console.ReadKey();
            PlayerPressedkey = Char.Parse(Hitkey.Key.ToString());
        }

        public void NotifyPalyerToAnInvalidAction(GameBoard game)
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

            SetPlayerWaitForMessage();
        }

        public string GetChoosedPlayerMenu(GameBoard game)
        {
            playerChoosedGameMenu = game.GameMenu[PlayerPressedkey];
            return PlayerChoosedGameMenu;
        }

        public string GetChoosedPlayerGameItem(GameBoard game)
        {
            playerChoosedGameItem = game.GameItems[PlayerPressedkey];
            return PlayerChoosedGameItem;
        }
    }
}
