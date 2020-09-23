using NUnit.Framework;
using System.Drawing;
using _05_Snake;
using System.Collections.Generic;

namespace _05_SnakeTest
{
    public class Tests
    {
        [Test]
        public void SnakeInitTest()
        {
            Snake snake = new Snake(new Point(2, 2), 3);

            var expected = new LinkedList<Point>();
            expected.AddFirst(new Point(2, 2));
            expected.AddFirst(new Point(2, 3));
            expected.AddFirst(new Point(2, 4));

            Assert.AreEqual(expected, snake.Coordinates);
        }

        [Test]
        public void SnakeShouldMoveDownTest()
        {
            Snake snake = new Snake(new Point(3, 3), 3);
            snake.Update(Direction.DOWN);

            var expected = new LinkedList<Point>();
            expected.AddFirst(new Point(3, 4));
            expected.AddFirst(new Point(3, 5));
            expected.AddFirst(new Point(3, 6));

            Assert.AreEqual(expected, snake.Coordinates);
        }

        [Test]
        public void SnakeShouldMoveLeftTest()
        {
            Snake snake = new Snake(new Point(3, 3), 3);
            snake.Update(Direction.LEFT);

            var expected = new LinkedList<Point>();
            expected.AddFirst(new Point(3, 4));
            expected.AddFirst(new Point(3, 5));
            expected.AddFirst(new Point(2, 5));

            Assert.AreEqual(expected, snake.Coordinates);
        }

        [Test]
        public void SnakeShouldMoveRightTest()
        {
            Snake snake = new Snake(new Point(3, 3), 3);
            snake.Update(Direction.RIGHT);

            var expected = new LinkedList<Point>();
            expected.AddFirst(new Point(3, 4));
            expected.AddFirst(new Point(3, 5));
            expected.AddFirst(new Point(4, 5));

            Assert.AreEqual(expected, snake.Coordinates);
        }

        [Test]
        public void SnakeShouldLeftAndMoveUpTest()
        {
            Snake snake = new Snake(new Point(3, 3), 3);
            snake.Update(Direction.LEFT);
            snake.Update(Direction.UP);

            var expected = new LinkedList<Point>();
            expected.AddFirst(new Point(3, 5));
            expected.AddFirst(new Point(2, 5));
            expected.AddFirst(new Point(2, 4));

            Assert.AreEqual(expected, snake.Coordinates);
        }

        [Test]
        public void SnakeShouldNotMoveRightIfAlreadyMovingLeftTest()
        {
            Snake snake = new Snake(new Point(3, 3), 3, Direction.LEFT);
            snake.Update(Direction.RIGHT);

            var expected = new LinkedList<Point>();
            expected.AddFirst(new Point(2, 3));
            expected.AddFirst(new Point(1, 3));
            expected.AddFirst(new Point(0, 3));

            Assert.AreEqual(expected, snake.Coordinates);
        }

        [Test]
        public void SnakeShouldNotMoveLeftIfAlreadyMovingRightTest()
        {
            Snake snake = new Snake(new Point(3, 3), 3, Direction.RIGHT);
            snake.Update(Direction.LEFT);

            var expected = new LinkedList<Point>();
            expected.AddFirst(new Point(4, 3));
            expected.AddFirst(new Point(5, 3));
            expected.AddFirst(new Point(6, 3));

            Assert.AreEqual(expected, snake.Coordinates);
        }

        [Test]
        public void SnakeShouldNotMoveUpIfAlreadyMovingDownTest()
        {
            Snake snake = new Snake(new Point(3, 3), 3, Direction.DOWN);
            snake.Update(Direction.UP);

            var expected = new LinkedList<Point>();
            expected.AddFirst(new Point(3, 4));
            expected.AddFirst(new Point(3, 5));
            expected.AddFirst(new Point(3, 6));

            Assert.AreEqual(expected, snake.Coordinates);
        }

        [Test]
        public void SnakeShouldNotMoveDownIfAlreadyMovingUpTest()
        {
            Snake snake = new Snake(new Point(3, 3), 3, Direction.UP);
            snake.Update(Direction.DOWN);

            var expected = new LinkedList<Point>();
            expected.AddFirst(new Point(3, 2));
            expected.AddFirst(new Point(3, 1));
            expected.AddFirst(new Point(3, 0));

            Assert.AreEqual(expected, snake.Coordinates);
        }

        [Test]
        public void SnakeShouldNotEatHimselfTest()
        {
            Snake snake = new Snake(new Point(3, 3), 5);
            snake.Update(Direction.LEFT);
            snake.Update(Direction.UP);
            snake.Update(Direction.RIGHT);

            Assert.AreEqual(false, snake.Update(Direction.DOWN));
        }
    }
}