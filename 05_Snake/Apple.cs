using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace _05_Snake
{
    class Apple
    {
        public static readonly char SYMBOL = '*';

        public Point Position { get; private set; }
        public int Score { get; private set; }

        public Apple(Point position, int score = 1)
        {
            Position = position;
            Score = score;
        }
    }
}
