using Mindmagma.Curses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace _05_Snake
{
    class Game
    {
        private Size size;

        private readonly Snake snake;
        private Apple apple;
        private readonly Random random;
        private bool isGameOver;
        public int PlayerScore { get; private set; }

        private readonly IntPtr screen;
        private static readonly int DELAY_MS = 200;
        private static readonly char SYMBOL_BORDER_HORIZONTAL = '-';
        private static readonly char SYMBOL_BORDER_VERTICAL = '|';

        public Game(Size size, int baseSnakeSize = 4)
        {
            this.size = size;

            random = new Random();
            snake = new Snake(new Point(size.Height / 2, size.Width / 2), baseSnakeSize);
            GenerateNewApple();

            PlayerScore = 0;
            isGameOver = false;

            screen = NCurses.InitScreen();
            NCurses.NoEcho();
        }

        internal void Loop()
        {
            Render();
            while(!isGameOver)
            {
                Thread.Sleep(DELAY_MS);
                Update();
                Render();
            }

            Console.ReadLine();
        }

        private void Update()
        {
            NCurses.GetChar();
            UpdateSnakeDirection(NCurses.GetChar());
            CheckEatApple();
            CheckSnakeOutOfBounds();
        }

        private void UpdateSnakeDirection(int key)
        {
            Direction newDirection = snake.SnakeDirection;
            switch (key)
            {
                case 77:
                    newDirection = Direction.RIGHT;
                    break;
                case 75:
                    newDirection = Direction.LEFT;
                    break;
                case 72:
                    newDirection = Direction.UP;
                    break;
                case 80:
                    newDirection = Direction.DOWN;
                    break;
            }

            if (!snake.Update(newDirection))
            {
                isGameOver = true;
            }
        }

        private void CheckEatApple()
        {
            if (IsSnakeOverPosition(apple.Position))
            {
                PlayerScore += apple.Score;
                GenerateNewApple();
                snake.HasEatenApple();
            }
        }

        private void CheckSnakeOutOfBounds()
        {
            Point snakeHead = snake.Coordinates.First.Value;
            if (snakeHead.Y < 0 || snakeHead.Y >= size.Height ||
                snakeHead.X < 0 || snakeHead.X >= size.Width)
            {
                isGameOver = true;
            }
        }

        private void Render()
        {
            NCurses.Clear();
            NCurses.NoDelay(screen, true);

            RenderBorders();
            RenderSnake();
            RenderApple();
            RenderScore();

            NCurses.Refresh();
        }

        private void RenderBorders()
        {
            for (int i = 0; i < size.Width + 2; i++)
            {
                NCurses.MoveAddChar(0, i, SYMBOL_BORDER_HORIZONTAL);
                NCurses.MoveAddChar(size.Height + 1, i, SYMBOL_BORDER_HORIZONTAL);
            }

            for (int i = 0; i < size.Height; i++)
            {
                NCurses.MoveAddChar(i + 1, 0, SYMBOL_BORDER_VERTICAL);
                NCurses.MoveAddChar(i + 1, size.Width + 1, SYMBOL_BORDER_VERTICAL);
            }
        }

        private void RenderSnake()
        {
            foreach (var coord in snake.Coordinates)
            {
                NCurses.MoveAddChar(coord.Y + 1, coord.X + 1, Snake.SYMBOL_BODY);
            }
            NCurses.MoveAddChar(snake.Coordinates.First.Value.Y + 1, snake.Coordinates.First.Value.X + 1, Snake.SYMBOL_HEAD);
        }

        private void RenderApple()
        {
            NCurses.MoveAddChar(apple.Position.Y + 1, apple.Position.X + 1, Apple.SYMBOL);
        }

        private void RenderScore()
        {
            NCurses.MoveAddString(size.Height + 2, 0, "Score : " + PlayerScore.ToString());
        }

        private void GenerateNewApple()
        {
            Point position;
            do
            {
                position = new Point(random.Next(size.Height), random.Next(size.Width));
            } while (IsSnakeOverPosition(position));


            apple = new Apple(position);
        }

        private bool IsSnakeOverPosition(Point position)
        {
            foreach (var coord in snake.Coordinates)
            {
                if (coord == position) { return true; }
            }

            return false;
        }

        ~Game()
        {
            NCurses.EndWin();
        }
    }
}
