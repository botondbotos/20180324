using System.Collections.Generic;

namespace Trivia
{
    public class Board
    {
        private readonly IUi ui;
        private readonly int size;
        public List<Field> fields;

        public Board(IUi ui, int size)
        {
            this.ui = ui;
            this.size = size;
            this.fields = new List<Field>(size);
        }

        public void AdvancePlayer(Player player, int roll)
        {
            player.Place = (player.Place + roll) % size;

            ui.PlayerMovesTo(player.Name, player.Place, this.fields[player.Place].QuestionCategory);
        }
    }

    public class Field
    {
        public List<Player> Players { get; set; }

        public string QuestionCategory { get; private set; }
    }
}