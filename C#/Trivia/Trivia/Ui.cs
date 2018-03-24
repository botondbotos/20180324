using System;

namespace Trivia
{
    public class Ui
    {
        public void PlayerAdded(string name, int count)
        {
            Console.WriteLine(name + " was added");
            Console.WriteLine("They are player number " + count);
        }

        public void PlayerRolls(string name, int roll)
        {
            Console.WriteLine(name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
        }

        public void PlayerLeavesPenalty(string name)
        {
            Console.WriteLine(name + " is getting out of the penalty box");
        }

        public void PlayerStaysInPenalty(string name)
        {
            Console.WriteLine(name + " is not getting out of the penalty box");
        }

        public void PlayerMovesTo(string name, int place, string category)
        {
            Console.WriteLine(name
                              + "'s new location is "
                              + place);
            Console.WriteLine("The category is " + category);
        }

        public void CorrectAnswer(string name, int purse)
        {
            Console.WriteLine("Answer was correct!!!!");
            Console.WriteLine(name
                              + " now has "
                              + purse
                              + " Gold Coins.");
        }

        public void IncorrectAnswer(string name)
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(name + " was sent to the penalty box");

        }
    }
}