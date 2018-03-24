using System;
using System.Collections.Generic;
using System.Linq;

namespace UglyTrivia
{
    public class Game
    {
        private readonly bool[] mInPenaltyBox = new bool[6];

        private readonly int[] mPlaces = new int[6];

        private readonly List<string> mPlayers = new List<string>();

        private readonly LinkedList<string> mPopQuestions = new LinkedList<string>();
        private readonly int[] mPurses = new int[6];
        private readonly LinkedList<string> mRockQuestions = new LinkedList<string>();
        private readonly LinkedList<string> mScienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> mSportsQuestions = new LinkedList<string>();
        private int mCurrentPlayer;
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
            mPlayers.Add(playerName);
            mPlaces[HowManyPlayers()] = 0;
            mPurses[HowManyPlayers()] = 0;
            mInPenaltyBox[HowManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + mPlayers.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return mPlayers.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(mPlayers[mCurrentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (mInPenaltyBox[mCurrentPlayer])
            {
                if (roll % 2 != 0)
                {
                    mIsGettingOutOfPenaltyBox = true;

                    Console.WriteLine(mPlayers[mCurrentPlayer] + " is getting out of the penalty box");
                    mPlaces[mCurrentPlayer] = mPlaces[mCurrentPlayer] + roll;
                    if (mPlaces[mCurrentPlayer] > 11) mPlaces[mCurrentPlayer] = mPlaces[mCurrentPlayer] - 12;

                    Console.WriteLine(mPlayers[mCurrentPlayer]
                                      + "'s new location is "
                                      + mPlaces[mCurrentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(mPlayers[mCurrentPlayer] + " is not getting out of the penalty box");
                    mIsGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                mPlaces[mCurrentPlayer] = mPlaces[mCurrentPlayer] + roll;
                if (mPlaces[mCurrentPlayer] > 11) mPlaces[mCurrentPlayer] = mPlaces[mCurrentPlayer] - 12;

                Console.WriteLine(mPlayers[mCurrentPlayer]
                                  + "'s new location is "
                                  + mPlaces[mCurrentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(mPopQuestions.First());
                mPopQuestions.RemoveFirst();
            }

            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(mScienceQuestions.First());
                mScienceQuestions.RemoveFirst();
            }

            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(mSportsQuestions.First());
                mSportsQuestions.RemoveFirst();
            }

            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(mRockQuestions.First());
                mRockQuestions.RemoveFirst();
            }
        }

        private string CurrentCategory()
        {
            if (mPlaces[mCurrentPlayer] == 0) return "Pop";
            if (mPlaces[mCurrentPlayer] == 4) return "Pop";
            if (mPlaces[mCurrentPlayer] == 8) return "Pop";
            if (mPlaces[mCurrentPlayer] == 1) return "Science";
            if (mPlaces[mCurrentPlayer] == 5) return "Science";
            if (mPlaces[mCurrentPlayer] == 9) return "Science";
            if (mPlaces[mCurrentPlayer] == 2) return "Sports";
            if (mPlaces[mCurrentPlayer] == 6) return "Sports";
            if (mPlaces[mCurrentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool WasCorrectlyAnswered()
        {
            if (mInPenaltyBox[mCurrentPlayer])
                if (mIsGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    mPurses[mCurrentPlayer]++;
                    Console.WriteLine(mPlayers[mCurrentPlayer]
                                      + " now has "
                                      + mPurses[mCurrentPlayer]
                                      + " Gold Coins.");

                    var winner = DidPlayerWin();
                    mCurrentPlayer++;
                    if (mCurrentPlayer == mPlayers.Count) mCurrentPlayer = 0;

                    return winner;
                }
                else
                {
                    mCurrentPlayer++;
                    if (mCurrentPlayer == mPlayers.Count) mCurrentPlayer = 0;
                    return true;
                }

            {
                Console.WriteLine("Answer was corrent!!!!");
                mPurses[mCurrentPlayer]++;
                Console.WriteLine(mPlayers[mCurrentPlayer]
                                  + " now has "
                                  + mPurses[mCurrentPlayer]
                                  + " Gold Coins.");

                var winner = DidPlayerWin();
                mCurrentPlayer++;
                if (mCurrentPlayer == mPlayers.Count) mCurrentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(mPlayers[mCurrentPlayer] + " was sent to the penalty box");
            mInPenaltyBox[mCurrentPlayer] = true;

            mCurrentPlayer++;
            if (mCurrentPlayer == mPlayers.Count) mCurrentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return !(mPurses[mCurrentPlayer] == 6);
        }
    }
}