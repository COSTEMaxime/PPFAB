using System;
using System.Collections.Generic;

namespace _03_TicTacToe
{
    public class BoardGenerator
    {
        public static char[,] Generate(int boardSize, char EMPTY_CHAR)
        {
            char[,] board = new char[boardSize, boardSize];

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    board[i, j] = EMPTY_CHAR;
                }
            }

            return board;
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