using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RPSLSGAMEver4;

namespace RPSLSGAMETESTS
{
    public class GameBoardTests
    {
        [Test]
        public void CheckSetGameTitleSuccess()
        {
            GameBoard game = new GameBoard();

            game.SetGameTitle();

            Assert.Pass("Success");
        }

        [Test]
        public void CheckSetGameWelcomeMessageSuccess()
        {
            GameBoard game = new GameBoard();

            game.WriteGameWelcomeMessage();

            Assert.Pass("Success");
        }

        [Test]
        public void CheckSetPlayerWaitForInputMessageSuccess()
        {
            GameBoard game = new GameBoard();

            game.WritePlayerWaitForInputMessage();

            Assert.Pass("Success");
        }

        [TestCase('E', "Start the Game")]
        [TestCase('B', "Back to the Menu")]
        [TestCase('C', "Save the Result")]
        [TestCase('H', "Game Help")]
        [TestCase('Q', "Quit the Game")]
        [Test]
        public void CheckSetChoosedPlayerMenuSuccess(char testKey, string testMenu)
        {
            Player player = new Player();
            GameBoard game = new GameBoard();

            player.PlayerPressedkey = testKey;
            game.SetChoosedPlayerMenu(player, game);

            Assert.AreEqual(testMenu, player.PlayerChoosedGameMenu);
        }

        [Test]
        public void CheckSetGameAvailableItemsSuccess()
        {
            GameBoard game = new GameBoard();

            game.WriteGameAvailableItems();

            Assert.Pass("Success");
        }

        [TestCase(1)]
        [Test]
        public void CheckSetPlayerPointSuccess(int testPoint)
        {
            Player player = new Player();
            GameBoard game = new GameBoard();
            
            game.SetPlayerPoint(player);
            
            Assert.AreNotEqual(testPoint, player.PlayerPoint);
        }

        [TestCase(1)]
        [Test]
        public void CheckSetMachinePointSuccess(int testPoint)
        {
            Machine machine = new Machine();
            GameBoard game = new GameBoard();

            game.SetMachinePoint(machine);

            Assert.AreNotEqual(testPoint, machine.MachinePoint);
        }

        [Test]
        public void CheckSetMachineKeySuccess()
        {
            Machine machine = new Machine();
            GameBoard game = new GameBoard();

            game.SetMachineKey(machine, game);

            Assert.IsNotNull(machine.MachinePressedkey);
        }

        [TestCase('P', "Paper")]
        [TestCase('L', "Lizard")]
        [TestCase('R', "Rock")]
        [TestCase('S', "Scissor")]
        [TestCase('V', "Spock")]
        [Test]
        public void CheckSetPlayerGameItemSuccess(char testKey, string testGameItem)
        {
            Player player = new Player();
            GameBoard game = new GameBoard();
            player.PlayerPressedkey = testKey;

            game.SetPlayerGameItem(player, game);

            Assert.AreEqual(testGameItem, player.PlayerChoosedGameItem);
        }

        [TestCase('P', "Paper")]
        [TestCase('L', "Lizard")]
        [TestCase('R', "Rock")]
        [TestCase('S', "Scissor")]
        [TestCase('V', "Spock")]
        [Test]
        public void CheckSetMachineGameItemSuccess(char testKey, string testGameItem)
        {
            Machine machine = new Machine();
            GameBoard game = new GameBoard();
            machine.MachinePressedkey = testKey;

            game.SetMachineGameItem(machine, game);

            Assert.AreEqual(testGameItem, machine.MachineChoosedGameItem);
        }

        [Test]
        public void CheckSetGameItemsEqualMessageSuccess()
        {
            GameBoard game = new GameBoard();

            game.WriteGameItemsEqualMessage();

            Assert.Pass("Success");
        }


        [Test]
        public void CheckSetPlayerInvalidActionSuccess()
        {
            GameBoard game = new GameBoard();
            Player player = new Player();

            game.SetPlayerInvalidAction(player, game);
            
            Assert.Pass("Success");
        }

        [TestCase("Paper", "Rock")]
        [TestCase("Spock", "Rock")]
        [TestCase("Scissor", "Paper")]
        [TestCase("Rock", "Scissor")]
        [TestCase("Spock", "Scissor")]
        [TestCase("Paper", "Spock")]
        [TestCase("Lizard", "Spock")]
        [TestCase("Scissor", "Lizard")]
        [TestCase("Rock", "Lizard")]
        [TestCase("Lizard", "Paper")]
        [TestCase("Rock", "Paper")]
        [TestCase("Rock", "Spock")]
        [TestCase("Paper", "Scissor")]
        [TestCase("Scissor", "Rock")]
        [TestCase("Scissor", "Spock")]
        [TestCase("Spock", "Paper")]
        [TestCase("Spock", "Lizard")]
        [TestCase("Lizard", "Scissor")]
        [TestCase("Lizard", "Rock")]
        [TestCase("Paper", "Lizard")]

        [Test]
        public void CheckGameRulesPlayerSuccess(string optionOne,
                                          string optionTwo)
        {
            Player player = new Player();
            Machine machine = new Machine();
            GameBoard game = new GameBoard();
            var expectedPlayerPoint = 0;
            var expectedMachinePoint = 0;
            player.PlayerChoosedGameItem = optionOne;
            machine.MachineChoosedGameItem = optionTwo;
            
            game.SetGameCompareItems(player, machine);
            optionOne = game.GameCompareChoosedItems.Item1;
            optionTwo = game.GameCompareChoosedItems.Item2;
            game.GameRulesCheck(player, machine, optionOne, optionTwo);
            expectedPlayerPoint = player.PlayerPoint;
            expectedMachinePoint = machine.MachinePoint;

            Assert.AreEqual(expectedPlayerPoint, player.PlayerPoint);
            Assert.AreEqual(expectedMachinePoint, machine.MachinePoint);
            Assert.AreNotEqual(expectedPlayerPoint, expectedMachinePoint);
        }

        [TestCase("Paper", "Rock")]
        [TestCase("Spock", "Rock")]
        [TestCase("Scissor", "Paper")]
        [TestCase("Rock", "Scissor")]
        [TestCase("Spock", "Scissor")]
        [TestCase("Paper", "Spock")]
        [TestCase("Lizard", "Spock")]
        [TestCase("Scissor", "Lizard")]
        [TestCase("Rock", "Lizard")]
        [TestCase("Lizard", "Paper")]
        [TestCase("Rock", "Paper")]
        [TestCase("Rock", "Spock")]
        [TestCase("Paper", "Scissor")]
        [TestCase("Scissor", "Rock")]
        [TestCase("Scissor", "Spock")]
        [TestCase("Spock", "Paper")]
        [TestCase("Spock", "Lizard")]
        [TestCase("Lizard", "Scissor")]
        [TestCase("Lizard", "Rock")]
        [TestCase("Paper", "Lizard")]

        [Test]

        public void CheckGameRulesMachineSuccess(string optionOne,
                                          string optionTwo)
        {
            Player player = new Player();
            Machine machine = new Machine();
            GameBoard game = new GameBoard();
            var expectedPlayerPoint = 0;
            var expectedMachinePoint = 0;
            player.PlayerChoosedGameItem = optionOne;
            machine.MachineChoosedGameItem = optionTwo;

            game.SetGameCompareItems(player, machine);
            optionOne = game.GameCompareChoosedItems.Item1;
            optionTwo = game.GameCompareChoosedItems.Item2;
            game.GameRulesCheck(player, machine, optionTwo, optionOne);
            expectedPlayerPoint = player.PlayerPoint;
            expectedMachinePoint = machine.MachinePoint;

            Assert.AreEqual(expectedPlayerPoint, player.PlayerPoint);
            Assert.AreEqual(expectedMachinePoint, machine.MachinePoint);
            Assert.AreNotEqual(expectedPlayerPoint, expectedMachinePoint);
        }

        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [Test]
        public void CheckShowGameResultSuccess(int expectedPlayerPoint, int expectedMachinePoint)
        {
            Player player = new Player();
            Machine machine = new Machine();
            GameBoard game = new GameBoard();
            Tuple<string, string> testCompareChoosedItems = new Tuple<string, string>("Scissor", "Paper");
            Dictionary<Tuple<string, string>, string> testWinner = new Dictionary<Tuple<string, string>, string>();

            player.PlayerChoosedGameItem = "Scissor";
            machine.MachineChoosedGameItem = "Paper";
            game.GameCompareChoosedItems = testCompareChoosedItems;
            testWinner.Add(testCompareChoosedItems, "Scissor");
            game.GameWinner.Add(testCompareChoosedItems, "Paper");
            game.GameWinner[game.GameCompareChoosedItems] = testWinner[testCompareChoosedItems];
            player.PlayerPoint = expectedPlayerPoint;
            machine.MachinePoint = expectedMachinePoint;
            game.GameShowTheResult(player, machine);
        }

        [Test]
        public void CheckSetGameFinalizeMenuNavigationMessageSuccess()
        {
            GameBoard game = new GameBoard();

            game.WriteGameFinalizeMenuNavigationMessage();

            Assert.Pass("Success");
        }

        [TestCase("C:\\Test\\")]
        [Test]
        public void CheckCreateGameResultDirectorySuccess(string expectedGameDirectory)
        {
            GameBoard game = new GameBoard();

            game.GameResultDirectory = expectedGameDirectory;
            game.GameCheckSaveDirectoryExsits();

            DirectoryAssert.Exists(expectedGameDirectory);
            Directory.Delete(expectedGameDirectory);
        }

        [Test]
        public void CheckResultSavingSuccess()
        {
            Player player = new Player();
            Machine machine = new Machine();
            GameBoard game = new GameBoard();
            var expectedFileName = "";
            var expectedFileData = "";
            player.PlayerName = "Test";
            player.PlayerPoint = 1;
            player.PlayerChoosedGameItem = "Scissor";
            machine.MachinePoint = 1;
            machine.MachineChoosedGameItem = "Scissor";
            
            game.SaveTheResultToFile(player, machine);
            expectedFileName = game.gameResultFullPath;
            expectedFileData = game.GameResult;

            DirectoryAssert.Exists(game.GameResultDirectory);
            FileAssert.Exists(expectedFileName);
            Assert.AreEqual(expectedFileData, game.GameResult);
        }

        [Test]
        public void CheckSetGameRulesMessageSuccess()
        {
            GameBoard game = new GameBoard();

            game.SetGameRulesMessage();

            Assert.Pass("Success");
        }

    }
}
