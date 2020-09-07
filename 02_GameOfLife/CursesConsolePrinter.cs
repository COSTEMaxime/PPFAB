using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindmagma.Curses;

namespace _02_GameOfLife
{
    class CursesConsolePrinter : BoardPrinter
    {
        private static IntPtr Screen;

        public void Init()
        {
            Screen = NCurses.InitScreen();
        }

        public void Print(Board board)
        {
            NCurses.NoDelay(Screen, true);
            NCurses.NoEcho();

            for (int i = 0; i < board.height; i++)
            {
                StringBuilder buffer = new StringBuilder();
                for (int j = 0; j < board.width; j++)
                {
                    buffer.Append(board.board[i, j] ? '■' : ' ');
                }
                NCurses.MoveAddString(i, 0, buffer.ToString());
            }
            NCurses.Refresh();
        }

        ~CursesConsolePrinter()
        {
            NCurses.EndWin();
        }
    }
}
