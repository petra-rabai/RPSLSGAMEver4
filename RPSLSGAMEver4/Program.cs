using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSLSGAMEver4
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard game = new GameBoard();
            Player player = new Player();
            Machine machine = new Machine();

            game.GameInitialize(player,machine,game);

        }
    }
}
