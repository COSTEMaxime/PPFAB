using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_TicTacToe
{
    public class WinningMoveIA : RandomIA
    {
        public WinningMoveIA(string name, char symbol, int boardSize) : base(name, symbol, boardSize)
        {
        }

        internal override Tuple<int, int> AskNextMove(char[,] board)
        {
            Tuple<int, int> move = findWinningMove(board, Symbol);
            return move == null ? randomMove(board) : move;
        }

        protected Tuple<int, int> findWinningMove(char[,] board, char symbol)
        {
            List<List<Tuple<int, int>>> boardLines = BoardGenerator.GetAllBoardLinesCoordinates(boardSize);

            foreach (var line in boardLines)
            {
                int currentLineScore = 0;
                int movesToWin = 0;
                Tuple<int, int> winCoords = null;

                foreach (var coord in line)
                {
                    if (board[coord.Item1, coord.Item2] == symbol)
                    {
                        currentLineScore++;
                    }
                    else if (board[coord.Item1, coord.Item2] == Engine.EMPTY_CHAR)
                    {
                        movesToWin++;
                        winCoords = coord;
                    }
                }

                if (currentLineScore == boardSize - 1 && movesToWin == 1) { return winCoords; }
            }

            return null;
        }
    } 
}
