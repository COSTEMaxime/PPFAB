using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_GameOfLife
{
    class ConsolePrinter : BoardPrinter
    {
        public void Init() {}

        public void Print(Board board)
        {
            Console.WriteLine(new string('-', board.width));
            for (int i = 0; i < board.height; i++)
            {
                StringBuilder buffer = new StringBuilder();
                for (int j = 0; j < board.width; j++)
                {
                    buffer.Append(board.board[i, j] ? '#' : ' ');
                }
                Console.WriteLine(buffer.ToString());
            }
            Console.WriteLine(new string('-', board.width));
        }
    }
}
