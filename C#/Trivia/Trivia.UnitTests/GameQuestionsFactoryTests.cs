using FluentAssertions;
using NUnit.Framework;

namespace Trivia.UnitTests
{
    public class GameQuestionsFactoryTests
    {
        [Test]
        public void GivenRegionIsIndia_CategoriesShouldBeHistoryAndNotContainRock()
        {
            // Arrange

            // Act
            var actualQustion = GameQuestionsFactory.CreateQuestions(GameRegion.India);

            // Assert
            actualQustion.Keys.Should().BeEquivalentTo("Pop", "Science", "Sports", "History");
        }
    }
}
