using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_TicTacToe
{
    public class RandomIA : IPlayer
    {
        protected int boardSize;
        private Random random;
        public RandomIA(string name, char symbol, int boardSize) : base(name, symbol)
        {
            this.boardSize = boardSize;
            random = new Random();
        }

        internal override Tuple<int, int> AskNextMove(char[,] board)
        {
            return randomMove(board);
        }

        protected Tuple<int, int> randomMove(char[,] board)
        {
            int x, y;
            do
            {
                x = random.Next(boardSize);
                y = random.Next(boardSize);
            } while (board[x, y] != Engine.EMPTY_CHAR);
            return new Tuple<int, int>(x, y);
        }

        protected char? getOponentSymbol(char[,] board)
        {
            foreach (char c in board)
            {
                if (c != Engine.EMPTY_CHAR && c != Symbol)
                {
                    return c;
                }
            }

            return null;
        }
    }
}
