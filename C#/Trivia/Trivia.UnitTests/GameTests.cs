using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia.UnitTests
{
    public class GameTests
    {
        public void GivenRegionIsIndia_CategoriesShouldBeHistoryAndNotContainRock()
        {
            // Arrange
            var questions = new Dictionary<string, LinkedList<string>>();

            // Act
            var game = new Game(new Ui(), new InfiniteTimer(), questions);

            // Assert
        }
    }
}
