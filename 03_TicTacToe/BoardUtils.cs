using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_TicTacToe
{
    class BoardUtils
    {
        public static Tuple<bool, char> ComputeGameResult(char[,] board, char emptyChar)
        {
            var boardLines = BoardUtils.GetAllBoardLinesCoordinates(board.GetLength(0));

            foreach (var line in boardLines)
            {
                char c = board[line.First().Item1, line.First().Item2];
                if (c == emptyChar) { continue; }

                bool win = true;
                foreach (var coord in line)
                {
                    if (c != board[coord.Item1, coord.Item2])
                    {
                        win = false;
                        break;
                    }
                }

                if (win)
                {
                    return new Tuple<bool, char>(true, c);
                }
            }

            if (BoardUtils.IsDraw(board, emptyChar))
            {
                return new Tuple<bool, char>(true, emptyChar);
            }

            return new Tuple<bool, char>(false, emptyChar);
        }

        internal static List<Tuple<int, int>> GetAllLegalMoves(char[,] board, char emptyChar)
        {
            List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == emptyChar) { moves.Add(new Tuple<int, int>(i, j)); }
                }
            }

            return moves;
        }

        private static bool IsDraw(char[,] board, char emptyChar)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == emptyChar) { return false; }
                }
            }

            return true;
        }

        public static List<List<Tuple<int, int>>> GetAllBoardLinesCoordinates(int boardSize)
        {
            List<List<Tuple<int, int>>> lines = new List<List<Tuple<int, int>>>();
            List<Tuple<int, int>> currentLine;
            // horizontal
            for (int i = 0; i < boardSize; i++)
            {
                currentLine = new List<Tuple<int, int>>();
                for (int j = 0; j < boardSize; j++)
                {
                    currentLine.Add(new Tuple<int, int>(i, j));
                }
                lines.Add(currentLine);
            }

            // vertical
            for (int j = 0; j < boardSize; j++)
            {
                currentLine = new List<Tuple<int, int>>();
                for (int i = 0; i < boardSize; i++)
                {
                    currentLine.Add(new Tuple<int, int>(i, j));
                }
                lines.Add(currentLine);
            }

            // diagonals
            currentLine = new List<Tuple<int, int>>();
            for (int i = 0; i < boardSize; i++)
            {
                currentLine.Add(new Tuple<int, int>(i, i));
            }
            lines.Add(currentLine);

            currentLine = new List<Tuple<int, int>>();

            for (int i = 0; i < boardSize; i++)
            {
                currentLine.Add(new Tuple<int, int>(i, boardSize - i - 1));
            }
            lines.Add(currentLine);

            return lines;
        }
    }
}
