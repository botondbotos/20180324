using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public static class GameQuestionsFactory
    {
        public static Dictionary<string, LinkedList<string>> CreateQuestions(GameRegion region)
        {
            var questions = new Dictionary<string, LinkedList<string>>
            {
                {"Pop", Generate("Pop")},
                {"Science", Generate("Science")},
                {"Sports", Generate("Sports")},
            };

            switch (region)
            {
                case GameRegion.India:
                    questions.Add("History", Generate("History"));
                    break;
                case GameRegion.Korea:
                    questions.Add("Rock", Generate("Rock"));
                    questions.Add("Literature", Generate("Literature"));
                    break;
                default:
                    questions.Add("Rock", Generate("Rock"));
                    break;
            }

            return questions;
        }

        private static LinkedList<string> Generate(string category)
        {
            return new LinkedList<string>(Enumerable.Range(0, 50).Select(i => $"{category} Question {i}"));
        }
    }
}