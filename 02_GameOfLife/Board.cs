using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_GameOfLife
{
    public class Board
    {
        public int width;
        public int height;
        public bool[,] board;

        internal bool SafeGet(int i, int j)
        {
            if (i < 0 || i == height || j < 0 || j == width) { return false; }
            else { return board[i, j]; }
        }

        internal Board DeepClone()
        {
            return new Board
            {
                width = width,
                height = height,
                board = board.Clone() as bool[,]
            };
        }

        public override bool Equals(object obj)
        {
            Board other = obj as Board;

            if (width != other.width) { return false; }
            if (height != other.height) { return false; }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (board[i, j] != other.board[i , j]) { return false; }
                }
            }

            return true;
        }
    };
}
