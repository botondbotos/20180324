using System;

namespace Trivia
{
    public class GameRunner
    {
        private static bool notAWinner;

        public static void Main(string[] args)
        {
            var ui = new Ui();
            var aGame = new Game(ui, new InfiniteTimer());

            aGame.Add(new Player("Chet", ui));
            aGame.Add(new Player("Pat", ui));
            aGame.Add(new Player("Sue", ui));

            var rand = new Random(int.Parse(args[0]));

            do
            {
                aGame.Roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                    notAWinner = aGame.WrongAnswer();
                else
                    notAWinner = aGame.WasCorrectlyAnswered();
            } while (notAWinner);
        }
    }
}