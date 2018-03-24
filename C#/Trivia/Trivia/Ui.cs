using System;

namespace Trivia
{
    public interface IUi
    {
        void PlayerAdded(string name, int count);
        void PlayerRolls(string name, int roll);
        void PlayerLeavesPenalty(string name);
        void PlayerStaysInPenalty(string name);
        void PlayerMovesTo(string name, int place, string category);
        void CorrectAnswer(string name, int purse);
        void IncorrectAnswer(string name);
        void PlayerWon(string name);
    }

    public class Ui : IUi
    {
        void IUi.PlayerAdded(string name, int count)
        {
            Console.WriteLine(name + " was added");
            Console.WriteLine("They are player number " + count);
        }

        void IUi.PlayerRolls(string name, int roll)
        {
            Console.WriteLine(name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
        }

        void IUi.PlayerLeavesPenalty(string name)
        {
            Console.WriteLine(name + " is getting out of the penalty box");
        }

        void IUi.PlayerStaysInPenalty(string name)
        {
            Console.WriteLine(name + " is not getting out of the penalty box");
        }

        void IUi.PlayerMovesTo(string name, int place, string category)
        {
            Console.WriteLine(name
                              + "'s new location is "
                              + place);
            Console.WriteLine("The category is " + category);
        }

        void IUi.CorrectAnswer(string name, int purse)
        {
            Console.WriteLine("Answer was correct!!!!");
            Console.WriteLine(name
                              + " now has "
                              + purse
                              + " Gold Coins.");
        }

        void IUi.IncorrectAnswer(string name)
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(name + " was sent to the penalty box");

        }

        void IUi.PlayerWon(string name)
        {
        }
    }
}