using System;
using System.Linq;

namespace RPSLSGAMEver4
{
    public class Machine : IMachine
    {

        public char MachinePressedkey { get; set; }
        public int MachinePoint { get; set; }
        public string MachineChoosedGameItem { get; set; }

        public char GetMachineKey(Board game)
        {
            Random choose = new Random();
            int chooseHelper = choose.Next(game.GameItems.Count);
            char gameDictionaryKey = game.GameItems.Keys.ElementAt(chooseHelper);
            MachinePressedkey = gameDictionaryKey;

            return MachinePressedkey;
        }

        public string GetChoosedMachineGameItem(Board game)
        {
            MachineChoosedGameItem = game.GameItems[MachinePressedkey];
            return MachineChoosedGameItem;
        }

        public int GetMachinePoint()
        {
            MachinePoint = 0;
            return MachinePoint;
        }

    }
}
