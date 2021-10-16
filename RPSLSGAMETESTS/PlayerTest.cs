using NUnit.Framework;
using RPSLSGAMEver4;

namespace RPSLSGAMETESTS
{
    public class PlayerTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [TestCase('E', "Start the Game")]
        [TestCase('B', "Back to the Menu")]
        [TestCase('C', "Save the Result")]
        [TestCase('H', "Game Help")]
        [TestCase('Q', "Quit the Game")]
        [Test]
        public void CheckPlayerChoosedFromMenuCollectionSuccess(char testKey, string testMenu)
        {
            Player player = new Player();
            GameBoard game = new GameBoard();

            player.PlayerPressedkey = testKey;
            player.GetChoosedPlayerMenu(game);

            Assert.AreEqual(testMenu,player.PlayerChoosedGameMenu);
        }

        [TestCase('P', "Paper")]
        [TestCase('L', "Lizard")]
        [TestCase('R', "Rock")]
        [TestCase('S', "Scissor")]
        [TestCase('V', "Spock")]
        [Test]
        public void CheckPlayerChoosedFromGameCollectionSuccess(char testKey, string testGameItem)
        {
            Player player = new Player();
            GameBoard game = new GameBoard();

            player.PlayerPressedkey = testKey;
            player.GetChoosedPlayerGameItem(game);

            Assert.AreEqual(testGameItem, player.PlayerChoosedGameItem);
        }

        [Test]
        public void CheckSetPlayerWaitForMessageSuccess()
        {
            Player player = new Player();

            player.SetPlayerWaitForMessage();

            Assert.Pass("Success");
        }

        [TestCase(1)]
        [Test]
        public void CheckGetPlayerPointSuccess(int testPoint)
        {
            Player player = new Player();

            player.GetPlayerPoint();

            Assert.AreNotEqual(testPoint, player.PlayerPoint);

        }

    }
}