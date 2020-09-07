using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _02_GameOfLife
{
    public class BoardGenerator
    {
        delegate bool FillFunction();

        public static Board LoadFromFile(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            Board board = new Board
            {
                width = lines[0].Length,
                height = lines.Length,
                board = new bool[lines.Length, lines[0].Length]
            };

            for (int i = 0; i < board.height; i++)
            {
                for (int j = 0; j < board.width; j++)
                {
                    board.board[i, j] = lines[i][j] != '0';
                }
            }

            return board;
        }

        public static Board CreateRandomStateBoard(int width, int height)
        {
            Random rndGenerator = new Random();
            return createStateBoard(width, height, () => Convert.ToBoolean(rndGenerator.Next(2)));
        }

        public static Board CreateAliveStateBoard(int width, int height)
        {
            return createStateBoard(width, height, () => true);
        }

        public static Board CreateDeadStateBoard(int width, int height)
        {
            return createStateBoard(width, height, () => false);
        }

        private static Board createStateBoard(int width, int height, FillFunction fillFunction)
        {
            Board board = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
            };

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    board.board[i,j] = fillFunction();
                }
            }

            return board;
        }
    }
}
