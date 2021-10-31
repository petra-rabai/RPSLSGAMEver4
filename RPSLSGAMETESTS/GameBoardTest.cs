using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RPSLSGAMEver4;
using RPSLSGAMEver4.Properties;

namespace RPSLSGAMETESTS
{
    public class BoardTests
    {
        [Test]
        public void CheckGameWelcomeScreenInitializeSuccess()
        {
            Board game = new Board();
            var expectedGameTitle = Resources.gameTitle;

            game.WelcomeScreenInitialize();

            Assert.AreEqual(expectedGameTitle,Resources.gameTitle);

        }

        [Test]
        public void CheckSetGameTitleSuccess()
        {
            Board game = new Board();

            game.SetGameTitle();

            Assert.Pass("Success");
        }

        [Test]
        public void CheckSetGameWelcomeMessageSuccess()
        {
            Board game = new Board();

            game.WriteGameWelcomeMessage();

            Assert.Pass("Success");
        }

        [Test]
        public void CheckSetPlayerWaitForInputMessageSuccess()
        {
            Board game = new Board();

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
            Board game = new Board();

            player.PlayerPressedkey = testKey;
            game.SetChoosedPlayerMenu(player, game);

            Assert.AreEqual(testMenu, player.PlayerChoosedGameMenu);
        }

        [Test]

        public void CheckGameStartSuccess()
        {
            Player player = new Player();
            Board game = new Board();
            Machine machine = new Machine();

            game.Start(player, machine, game);

            Assert.Pass("Success");

        }

        [Test]
        public void CheckSetGameAvailableItemsSuccess()
        {
            Board game = new Board();

            game.WriteGameAvailableItems();

            Assert.Pass("Success");
        }

        [TestCase(1)]
        [Test]
        public void CheckSetPlayerPointSuccess(int testPoint)
        {
            Player player = new Player();
            Board game = new Board();
            
            game.SetPlayerPoint(player);
            
            Assert.AreNotEqual(testPoint, player.PlayerPoint);
        }

        [TestCase(1)]
        [Test]
        public void CheckSetMachinePointSuccess(int testPoint)
        {
            Machine machine = new Machine();
            Board game = new Board();

            game.SetMachinePoint(machine);

            Assert.AreNotEqual(testPoint, machine.MachinePoint);
        }

        [Test]
        public void CheckSetMachineKeySuccess()
        {
            Machine machine = new Machine();
            Board game = new Board();

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
            Board game = new Board();
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
            Board game = new Board();
            machine.MachinePressedkey = testKey;

            game.SetMachineGameItem(machine, game);

            Assert.AreEqual(testGameItem, machine.MachineChoosedGameItem);
        }

        [Test]
        public void CheckSetGameItemsEqualMessageSuccess()
        {
            Board game = new Board();

            game.WriteGameItemsEqualMessage();

            Assert.Pass("Success");
        }


        [Test]
        public void CheckSetPlayerInvalidActionSuccess()
        {
            Board game = new Board();
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

        [Test]
        public void CheckThePlayerWinnerLogic(string optionOne,string optionTwo)
        {
            Board game = new Board();
            Player player = new Player();
            Machine machine = new Machine();
            var expectedplayerWinner = optionOne;
            player.PlayerChoosedGameItem = optionOne;
            machine.MachineChoosedGameItem = optionTwo;
            game.SetGameCompareItems(player, machine);
            game.RuleValidator(optionOne,optionTwo);
            game.GetTheGameWinner(player, machine, optionOne, optionTwo);

            Assert.AreEqual(expectedplayerWinner, game.GameWinner[game.GameCompareChoosedItems]);
            
        }

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
        public void CheckTheMachineWinnerLogic(string optionOne, string optionTwo)
        {
            Board game = new Board();
            Player player = new Player();
            Machine machine = new Machine();
            var expectedplayerWinner = optionTwo;
            player.PlayerChoosedGameItem = optionOne;
            machine.MachineChoosedGameItem = optionTwo;
            game.SetGameCompareItems(player, machine);
            game.RuleValidator(optionOne, optionTwo);
            game.GetTheGameWinner(player, machine, optionOne, optionTwo);

            Assert.AreEqual(expectedplayerWinner, game.GameWinner[game.GameCompareChoosedItems]);
        }


        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [Test]
        public void CheckShowGameResultSuccess(int expectedPlayerPoint, int expectedMachinePoint)
        {
            Player player = new Player();
            Machine machine = new Machine();
            Board game = new Board();
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
            game.ShowTheResult(player, machine);
        }

        [Test]
        public void CheckSetGameFinalizeMenuNavigationMessageSuccess()
        {
            Board game = new Board();

            game.WriteGameFinalizeMenuNavigationMessage();

            Assert.Pass("Success");
        }

        [TestCase("C:\\Test\\")]
        [Test]
        public void CheckCreateGameResultDirectorySuccess(string expectedGameDirectory)
        {
            Board game = new Board();

            game.GameResultDirectory = expectedGameDirectory;
            game.CheckSaveDirectoryExsits();

            DirectoryAssert.Exists(expectedGameDirectory);
            Directory.Delete(expectedGameDirectory);
        }

        [Test]
        public void CheckResultSavingSuccess()
        {
            Player player = new Player();
            Machine machine = new Machine();
            Board game = new Board();
            var expectedFileName = "";
            var expectedFileData = "";
            player.PlayerName = "Test";
            player.PlayerPoint = 1;
            player.PlayerChoosedGameItem = "Scissor";
            machine.MachinePoint = 1;
            machine.MachineChoosedGameItem = "Scissor";
            
            game.SaveTheResultToFile(player, machine);
            expectedFileName = game.ResultFullPath;
            expectedFileData = game.GameResult;

            DirectoryAssert.Exists(game.GameResultDirectory);
            FileAssert.Exists(expectedFileName);
            Assert.AreEqual(expectedFileData, game.GameResult);
        }

        [Test]
        public void CheckSetGameRulesMessageSuccess()
        {
            Board game = new Board();

            game.SetGameRulesMessage();

            Assert.Pass("Success");
        }

        [Test]
        public void CheckWriteHelpMenuNavigationMessageSuccess()
        {
            Board game = new Board();

            game.WriteHelpMenuNavigationMessage();

            Assert.Pass("Success");
        }

    }
}
