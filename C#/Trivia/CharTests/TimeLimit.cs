using NUnit.Framework;
using Trivia;

namespace CharTests
{
    [TestFixture]
    public class TimeLimit
    {
        private Game mGame;
        private TestTimer mTimer;
        private Ui mUi;
        private Player mPlayer1;
        private Player mPlayer2;

        [SetUp]
        public void SetUp()
        {
            mUi = new Ui();
            mTimer = new TestTimer();
            mGame = new Game(mUi, mTimer);
            mPlayer1 = new Player("p1", mUi);
            mGame.Add(mPlayer1);
            mPlayer2 = new Player("p2", mUi);
            mGame.Add(mPlayer2);
        }

        [Test]
        [TestCase(0, null, 0, 0)]
        [TestCase(1, null, 1, 0)]
        [TestCase(2, null, 1, 1)]
        [TestCase(10, null, 5, 5)]
        [TestCase(11, "p1", 6, 5)]
        [TestCase(12, "p1", 6, 6)]
        [TestCase(13, "p1", 7, 6)]
        public void When_PlayerGainsSixGoldBeforeTimeout_PlayerWins(int iterations, string winner, int p1gold, int p2gold)
        {
            bool @continue = true;
            for (var i = 0; i < iterations; ++i)
            {
                mGame.Roll(1);
                @continue = mGame.WasCorrectlyAnswered();
            }

            Assert.That(mPlayer1.Gold, Is.EqualTo(p1gold), "Player 1 gold");
            Assert.That(mPlayer2.Gold, Is.EqualTo(p2gold), "Player 2 gold");
            Assert.That(@continue, Is.EqualTo(winner == null), "Winner is {0}", winner);
        }

        [Test]
        public void When_NoPlayerGainsSixGoldBeforeTimeout_PlayerWithMostGoldWins()
        {
        }

        [Test]
        public void When_NoPlayerGainsSixGoldBeforeTimeoutAndIsADraw_NobodyWins()
        {
        }
    }
}