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
    }
}