using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSLSGAMEver4
{
    public class Machine
    {

        public char MachinePressedkey { get => machinePressedkey; set => machinePressedkey = value; }
        public int MachinePoint { get => machinePoint; set => machinePoint = value; }
        public string MachineChoosedGameItem { get => machineChoosedGameItem; set => machineChoosedGameItem = value; }

        private char machinePressedkey;
        private int machinePoint;
        private string machineChoosedGameItem;

        public char GetMachineKey(GameBoard game)
        {
            Random choose = new Random();
            int chooseHelper = choose.Next(game.GameItems.Count);
            char gameDictionaryKey = game.GameItems.Keys.ElementAt(chooseHelper);
            machinePressedkey = gameDictionaryKey;

            return MachinePressedkey;
        }

        public string GetChoosedMachineGameItem(GameBoard game)
        {
            machineChoosedGameItem = game.GameItems[MachinePressedkey];
            return MachineChoosedGameItem;
        }

        public int GetMachinePoint()
        {
            machinePoint = 0;
            return MachinePoint;
        }

    }
}
