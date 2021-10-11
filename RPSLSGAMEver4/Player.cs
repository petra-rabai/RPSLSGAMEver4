﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSLSGAMEver4
{
    public class Player
    {
        private string playerName = "";
        private string playerChoosedGameMenu = "";
        public int playerPoint;
        private char playerPressedkey;
        public string playerChoosedOption = "";
        readonly GameBoard game = new GameBoard();

        public string PlayerName { get => playerName; set => playerName = value; }
        public string PlayerChoosedGameMenu { get => playerChoosedGameMenu; set => playerChoosedGameMenu = value; }
        public char PlayerPressedkey { get => playerPressedkey; set => playerPressedkey = value; }

        public char GetPlayerKey()
        {
            Console.WriteLine(Properties.Resources.playerWaitForInputMessage);
            ReadKeyboard();
            while ((!game.GameMenu.ContainsKey(PlayerPressedkey)) && (!game.GameItems.ContainsKey(PlayerPressedkey)))
            {
                NotifyPalyerToAnInvalidAction();
                ReadKeyboard();
            }

            return PlayerPressedkey;
        }

        public string GetPlayerName()
        {
            Console.WriteLine(Properties.Resources.playerAddNameMessage);
            Console.WriteLine(Properties.Resources.playerWaitForInputMessage);
            playerName = Console.ReadLine();

            return PlayerName;
        }

        public void ReadKeyboard()
        {
            ConsoleKeyInfo Hitkey = Console.ReadKey();
            PlayerPressedkey = Char.Parse(Hitkey.Key.ToString());
        }

        public void NotifyPalyerToAnInvalidAction()
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

            Console.WriteLine(Properties.Resources.playerWaitForInputMessage);
        }

        public string GetChoosedPlayerMenu()
        {
            playerChoosedGameMenu = game.GameMenu[PlayerPressedkey];
            return PlayerChoosedGameMenu;
        }
    }
}
