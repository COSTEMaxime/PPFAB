using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_GameOfLife
{
    public class Simulation
    {
        private Board board;

        public Simulation(Board board)
        {
            this.board = board;
        }

        public Board NextState()
        {
            Board bufferBoard = board.DeepClone();

            for (int i = 0; i < board.height; i++)
            {
                for (int j = 0; j < board.width; j++)
                {
                    int neighborsCount = getNeighborsCount(i, j);
                    if (board.board[i,j])
                    {
                        // cell is alive
                        if (neighborsCount < 2 || neighborsCount > 3)
                        {
                            // underpopulation / overpopulation
                            bufferBoard.board[i, j] = false;
                        }
                    }
                    else
                    {
                        // dead cell
                        if (neighborsCount == 3)
                        {
                            bufferBoard.board[i, j] = true;
                        }
                    }
                }
            }

            board = bufferBoard;
            return board;
        }

        private int getNeighborsCount(int i, int j)
        {
            int count = 0;

            if (board.SafeGet(i - 1, j - 1)) { count++; }
            if (board.SafeGet(i - 1, j)) { count++; }
            if (board.SafeGet(i - 1, j + 1)) { count++; }
            if (board.SafeGet(i, j - 1)) { count++; }
            if (board.SafeGet(i, j + 1)) { count++; }
            if (board.SafeGet(i + 1, j - 1)) { count++; }
            if (board.SafeGet(i + 1, j)) { count++; }
            if (board.SafeGet(i + 1, j + 1)) { count++; }

            return count;
        }
    }
}
