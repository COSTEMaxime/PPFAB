using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_TicTacToe
{
    public class RandomIAPlayer : IPlayer
    {
        private int boardSize;
        private Random random;
        public RandomIAPlayer(string name, char symbol, int boardSize) : base(name, symbol)
        {
            this.boardSize = boardSize;
            random = new Random();
        }

        internal override Tuple<int, int> AskNextMove()
        {
            return new Tuple<int, int>(random.Next(boardSize), random.Next(boardSize));
        }
    }
}
