using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;

namespace _05_Snake
{
    public enum Direction
    {
        UP = 0,
        RIGHT,
        DOWN,
        LEFT
    }

    public class Snake
    {
        public static readonly char SYMBOL_BODY = 'O';
        public static readonly char SYMBOL_HEAD = 'X';

        public Direction SnakeDirection { get; private set; }
        public LinkedList<Point> Coordinates { get; private set; }
        bool shouldRemoveLast;

        public Snake(Point position, int baseSnakeSize, Direction defaultDirection = Direction.DOWN)
        {
            Coordinates = new LinkedList<Point>();
            Coordinates.AddFirst(position);

            SnakeDirection = defaultDirection;
            shouldRemoveLast = true;
            InitSnake(baseSnakeSize);
        }

        private void InitSnake(int baseSnakeSize)
        {
            while (Coordinates.Count() < baseSnakeSize)
            {
                Coordinates.AddFirst(GetNextCoordinateFromDirection());
            }
        }

        public bool Update(Direction newDirection)
        {
            if (IsNewDirectionAllowed(newDirection))
            {
                SnakeDirection = newDirection;
            }

            bool isMoveLegal = true;
            Point newHeadPosition = GetNextCoordinateFromDirection();

            foreach (Point coord in Coordinates)
            {
                if (newHeadPosition == coord && coord != Coordinates.Last.Value) { isMoveLegal = false; }
            }

            Coordinates.AddFirst(newHeadPosition);
            if (shouldRemoveLast)
            {
                Coordinates.RemoveLast();
            }
            shouldRemoveLast = true;

            return isMoveLegal;
        }

        private bool IsNewDirectionAllowed(Direction newDirection)
        {
            return ((int)SnakeDirection + (int)newDirection) % 2 != 0;
        }

        private Point GetNextCoordinateFromDirection()
        {
            Point newCoordinate = new Point(Coordinates.First.Value.X, Coordinates.First.Value.Y);
            switch (SnakeDirection)
            {
                case Direction.UP:
                    newCoordinate.Y--;
                    break;
                case Direction.DOWN:
                    newCoordinate.Y++;
                    break;
                case Direction.LEFT:
                    newCoordinate.X--;
                    break;
                case Direction.RIGHT:
                    newCoordinate.X++;
                    break;
            }

            return newCoordinate;
        }

        internal void HasEatenApple()
        {
            shouldRemoveLast = false;
        }
    }
}
