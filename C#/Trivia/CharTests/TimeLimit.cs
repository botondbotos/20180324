using Moq;
using NUnit.Framework;
using Trivia;

namespace CharTests
{
    [TestFixture]
    public class TimeLimit
    {
        private Game mGame;
        private TestTimer mTimer;
        private Mock<IUi> mUi;
        private Player mPlayer1;
        private Player mPlayer2;

        [SetUp]
        public void SetUp()
        {
            mUi = new Mock<IUi>(MockBehavior.Loose);
            mUi.Setup(u => u.PlayerWon(It.IsAny<string>())).Verifiable();

            var ui = mUi.Object;

            mTimer = new TestTimer();
            mGame = new Game(ui, new TestTimer(), GameQuestionsFactory.CreateQuestions(GameRegion.RestOfTheWorld));
            mPlayer1 = new Player("p1", ui);
            mGame.Add(mPlayer1);
            mPlayer2 = new Player("p2", ui);
            mGame.Add(mPlayer2);
        }

        [Test]
        [TestCase(0, null, 0, 0)]
        [TestCase(1, null, 1, 0)]
        [TestCase(2, null, 1, 1)]
        [TestCase(10, null, 5, 5)]
        [TestCase(11, "p1", 6, 5)]
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

            if (winner == null)
            {
                mUi.Verify(u => u.PlayerWon(It.IsAny<string>()), Times.Never);
            }
            else
            {
                mUi.Verify(u => u.PlayerWon(It.IsAny<string>()), Times.Exactly(1));
                mUi.Verify(u => u.PlayerWon(winner), Times.Exactly(1));
            }
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