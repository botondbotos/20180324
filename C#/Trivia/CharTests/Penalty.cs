using System.Collections.Generic;
using NUnit.Framework;
using Trivia;

namespace CharTests
{
    [TestFixture]
    public class Penalty
    {
        [Test]
        public void On_Start_PlayerIsNotInPenaltyBox()
        {
            var ui = new Ui();
            var player = new Player("dummy", ui);
            var game = new Game(ui, new TestTimer(), GameQuestionsFactory.CreateQuestions(GameRegion.RestOfTheWorld));
            game.Add(player);

            Assert.That(player.IsInPenalty, Is.False);
        }

        [Test]
        public void On_WrongAnswer_EnterPenaltyBox()
        {
            var ui = new Ui();
            var player = new Player("dummy", ui);
            var game = new Game(ui, new TestTimer(), GameQuestionsFactory.CreateQuestions(GameRegion.RestOfTheWorld));
            game.Add(player);

            game.Roll(1);
            game.WrongAnswer();
            Assert.That(player.IsInPenalty, Is.True);
        }

        [Test]
        public void On_CorrectAnswer_DontEnterPenaltyBox()
        {
            var ui = new Ui();
            var player = new Player("dummy", ui);
            var game = new Game(ui, new TestTimer(), GameQuestionsFactory.CreateQuestions(GameRegion.RestOfTheWorld));
            game.Add(player);

            game.Roll(1);
            game.WasCorrectlyAnswered();
            Assert.That(player.IsInPenalty, Is.False);
        }
    }
}