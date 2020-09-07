using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_GameOfLife
{
    interface BoardPrinter
    {
        void Init();
        void Print(Board board);
    }
}
