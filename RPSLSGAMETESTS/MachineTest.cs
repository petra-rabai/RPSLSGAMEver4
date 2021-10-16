using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPSLSGAMEver4;
using NUnit.Framework;

namespace RPSLSGAMETESTS
{
    public class MachineTests
    {
        [Test]
        public void CheckGetMachineKeySuccess()
        {
            Machine machine = new Machine();
            GameBoard game = new GameBoard();

            machine.GetMachineKey(game);

            Assert.IsNotNull(machine.MachinePressedkey);

        }

        [TestCase('P', "Paper")]
        [TestCase('L', "Lizard")]
        [TestCase('R', "Rock")]
        [TestCase('S', "Scissor")]
        [TestCase('V', "Spock")]
        [Test]
        public void CheckMachineChoosedFromGameCollectionSuccess(char testKey, string testGameItem)
        {
            Machine machine = new Machine();
            GameBoard game = new GameBoard();

            machine.MachinePressedkey = testKey;
            machine.GetChoosedMachineGameItem(game);

            Assert.AreEqual(testGameItem, machine.MachineChoosedGameItem);
        }

        [TestCase(1)]
        [Test]
        public void CheckGetMachinePointSuccess(int testPoint)
        {
            Machine machine = new Machine();

            machine.GetMachinePoint();

            Assert.AreNotEqual(testPoint, machine.MachinePoint);

        }

    }
}
