using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_TicTacToe
{
    public class SmartWinningMoveIA : WinningMoveIA
    {
        public SmartWinningMoveIA(string name, char symbol, int boardSize) : base(name, symbol, boardSize)
        {
        }

        internal override Tuple<int, int> AskNextMove(char[,] board)
        {
            Tuple<int, int> move = findWinningMove(board, Symbol);
            if (move != null)
            {
                return move;
            }

            char? oponentSymbol = getOponentSymbol(board);
            // oponent hasn't played yet, just return a random move
            if (oponentSymbol == null)
            {
                return randomMove(board);
            }

            move = findWinningMove(board, (char)oponentSymbol);
            return move == null ? randomMove(board) : move;
        }
    }
}
