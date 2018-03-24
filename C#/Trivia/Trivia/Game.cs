using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly Ui mUi = new Ui();

        private readonly List<Player> mPlayers = new List<Player>();
        private readonly int[] mPlaces = new int[6];
        private readonly bool[] mInPenaltyBox = new bool[6];
        private readonly int[] mPurses = new int[6];

        private readonly LinkedList<string> mPopQuestions = new LinkedList<string>();
        private readonly LinkedList<string> mRockQuestions = new LinkedList<string>();
        private readonly LinkedList<string> mScienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> mSportsQuestions = new LinkedList<string>();
        private int mCurrentPlayerIndex;
        private Player mCurrentPlayer;
        private bool mIsGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                mPopQuestions.AddLast("Pop Question " + i);
                mScienceQuestions.AddLast("Science Question " + i);
                mSportsQuestions.AddLast("Sports Question " + i);
                mRockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return HowManyPlayers() >= 2;
        }

        public bool Add(string playerName)
        {
            mPlayers.Add(new Player(playerName));
            mPlaces[HowManyPlayers()] = 0;
            mPurses[HowManyPlayers()] = 0;
            mInPenaltyBox[HowManyPlayers()] = false;

            mUi.PlayerAdded(playerName, mPlayers.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return mPlayers.Count;
        }

        public void Roll(int roll)
        {
            mCurrentPlayer = mPlayers[mCurrentPlayerIndex];

            var playerName = mCurrentPlayer.Name;
            mUi.PlayerRolls(playerName, roll);

            if (mInPenaltyBox[mCurrentPlayerIndex])
            {
                if (roll % 2 != 0)
                {
                    mIsGettingOutOfPenaltyBox = true;

                    mUi.PlayerLeavesPenalty(playerName);
                    MoveNext(roll, playerName);
                }
                else
                {
                    mUi.PlayerStaysInPenalty(playerName);
                    mIsGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                MoveNext(roll, playerName);
            }
        }

        private void MoveNext(int roll, string playerName)
        {
            mPlaces[mCurrentPlayerIndex] = mPlaces[mCurrentPlayerIndex] + roll;
            if (mPlaces[mCurrentPlayerIndex] > 11) mPlaces[mCurrentPlayerIndex] = mPlaces[mCurrentPlayerIndex] - 12;

            mUi.PlayerMovesTo(playerName, mPlaces[mCurrentPlayerIndex], CurrentCategory());
            AskQuestion();
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop") Question(mPopQuestions);
            if (CurrentCategory() == "Science") Question(mScienceQuestions);
            if (CurrentCategory() == "Sports") Question(mSportsQuestions);
            if (CurrentCategory() == "Rock") Question(mRockQuestions);
        }

        private static void Question(LinkedList<string> questions)
        {
            Console.WriteLine(questions.First());
            questions.RemoveFirst();
        }

        private string CurrentCategory()
        {
            var place = mPlaces[mCurrentPlayerIndex];
            if (place == 0) return "Pop";
            if (place == 1) return "Science";
            if (place == 2) return "Sports";
            if (place == 4) return "Pop";
            if (place == 5) return "Science";
            if (place == 6) return "Sports";
            if (place == 8) return "Pop";
            if (place == 9) return "Science";
            if (place == 10) return "Sports";
            return "Rock";
        }

        public bool WasCorrectlyAnswered()
        {
            if (mInPenaltyBox[mCurrentPlayerIndex])
                if (mIsGettingOutOfPenaltyBox)
                {
                    mPurses[mCurrentPlayerIndex]++;
                    mUi.CorrectAnswer(mCurrentPlayer.Name, mPurses[mCurrentPlayerIndex]);

                    var winner = PlayerDidNotWin();
                    NextPlayer();

                    return winner;
                }
                else
                {
                    NextPlayer();
                    return true;
                }

            {
                mPurses[mCurrentPlayerIndex]++;
                mUi.CorrectAnswerTypo(mCurrentPlayer.Name, mPurses[mCurrentPlayerIndex]);

                var winner = PlayerDidNotWin();
                NextPlayer();

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            mUi.IncorrectAnswer(mCurrentPlayer.Name);
            mInPenaltyBox[mCurrentPlayerIndex] = true;

            NextPlayer();
            return true;
        }

        private void NextPlayer()
        {
            mCurrentPlayerIndex++;
            if (mCurrentPlayerIndex == mPlayers.Count) mCurrentPlayerIndex = 0;
        }


        private bool PlayerDidNotWin()
        {
            return mPurses[mCurrentPlayerIndex] != 6;
        }
    }
}