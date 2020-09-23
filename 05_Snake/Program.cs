using Mindmagma.Curses;
using System;
using System.Threading;

namespace _05_Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new System.Drawing.Size(20, 20));
            game.Loop();
        }
    }
}
