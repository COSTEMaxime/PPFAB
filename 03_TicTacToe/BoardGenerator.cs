using System;

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
    }
}