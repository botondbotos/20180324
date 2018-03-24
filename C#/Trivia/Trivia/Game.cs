using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly IUi mUi;
        private readonly ITimer mTimer;
        private readonly Dictionary<string, LinkedList<string>> qustions;

        private readonly List<Player> mPlayers = new List<Player>();

        private int mCurrentPlayerIndex;
        private Player mCurrentPlayer;
        private bool mIsGettingOutOfPenaltyBox;

        public Game(
            IUi ui, 
            ITimer timer, 
            Dictionary<string, LinkedList<string>> qustions)
        {
            mUi = ui;
            mTimer = timer;
            this.qustions = qustions;
        }

        public bool Add(Player player)
        {
            mPlayers.Add(player);

            mUi.PlayerAdded(player.Name, mPlayers.Count);
            return true;
        }

        public void Roll(int roll)
        {
            mCurrentPlayer = mPlayers[mCurrentPlayerIndex];

            var playerName = mCurrentPlayer.Name;
            mUi.PlayerRolls(playerName, roll);

            if (mCurrentPlayer.IsInPenalty)
            {
                if (roll % 2 != 0)
                {
                    mIsGettingOutOfPenaltyBox = true;

                    mUi.PlayerLeavesPenalty(playerName);
                    Advance(roll);
                }
                else
                {
                    mUi.PlayerStaysInPenalty(playerName);
                    mIsGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                Advance(roll);
            }
        }

        private void Advance(int roll)
        {
            mCurrentPlayer.Advance(roll);
            AskQuestion();
        }

        private void AskQuestion()
        {
            var currentCategory = mCurrentPlayer.CurrentCategory();
            if (currentCategory == "Pop") Question(this.qustions["Pop"]);
            if (currentCategory == "Science") Question(this.qustions["Science"]);
            if (currentCategory == "Sports") Question(this.qustions["Sports"]);
            if (currentCategory == "Rock") Question(this.qustions["Rock"]);
            if (currentCategory == "History") Question(this.qustions["History"]);
            if (currentCategory == "Literature") Question(this.qustions["Literature"]);
        }

        private static void Question(LinkedList<string> questions)
        {
            Console.WriteLine(questions.First());
            questions.RemoveFirst();
        }

        public bool WasCorrectlyAnswered()
        {
            if (mCurrentPlayer.IsInPenalty && !mIsGettingOutOfPenaltyBox)
            {
                NextPlayer();
                return true;
            }

            mCurrentPlayer.CorrectAnswer();

            var winner = !mCurrentPlayer.Won;
            if (!winner) mUi.PlayerWon(mCurrentPlayer.Name);
            NextPlayer();

            return winner;
        }

        public bool WrongAnswer()
        {
            mCurrentPlayer.WrongAnswer();

            NextPlayer();
            return true;
        }

        private void NextPlayer()
        {
            mCurrentPlayerIndex++;
            if (mCurrentPlayerIndex == mPlayers.Count) mCurrentPlayerIndex = 0;
        }
    }

    public interface ITimer
    {
        bool Timeout { get; }
    }
}