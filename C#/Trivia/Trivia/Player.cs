namespace Trivia
{
    public class Player
    {
        private readonly IUi mUi;

        public string Name { get; }
        public int Place { get; set; }

        public Player(string name, IUi ui)
        {
            Name = name;
            mUi = ui;
        }

        

        public string CurrentCategory()
        {
            var place = Place;
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

        public void GainGold()
        {
            Gold++;
        }

        public int Gold { get; private set; }

        public bool Won => Gold == 6;

        public void CorrectAnswer()
        {
            GainGold();
            mUi.CorrectAnswer(Name, Gold);
        }

        public void WrongAnswer()
        {
            mUi.IncorrectAnswer(Name);
            IsInPenalty = true;
        }

        public bool IsInPenalty { get; private set; }
    }
}