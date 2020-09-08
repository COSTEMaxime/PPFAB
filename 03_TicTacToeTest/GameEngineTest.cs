using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _03_TicTacToe;
using System.Security.Authentication.ExtendedProtection;

namespace _03_TicTacToeTest
{
    [TestClass]
    public class GameEngineTest
    {
        [TestMethod]
        public void BoardGeneratorTest()
        {
            const int size = 5;
            char[,] expected = new char[size, size]
            {
                { ' ', ' ', ' ', ' ', ' ' },
                { ' ', ' ', ' ', ' ', ' ' },
                { ' ', ' ', ' ', ' ', ' ' },
                { ' ', ' ', ' ', ' ', ' ' },
                { ' ', ' ', ' ', ' ', ' ' }
            };

            CollectionAssert.AreEqual(expected, BoardGenerator.Generate(size, ' '));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Board should be a square")]
        public void ShouldRaiseIfBoardIsNotASquare()
        {
            char[,] board = new char[2, 3]
            {
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' },
            };

            Engine engine = new Engine(new RandomIA("AI_1", 'X', 2), new RandomIA("AI_2", 'O', 2), board);
        }

        [TestMethod]
        public void DrawTest1()
        {
            const int size = 3;
            char[,] board = new char[size, size]
            {
                { 'X', 'O', 'X' },
                { 'X', 'X', 'O' },
                { 'O', 'X', 'O' }
            };

            IPlayer player1 = new RandomIA("AI_1", 'X', size);
            IPlayer player2 = new RandomIA("AI_2", 'O', size);
            Engine engine = new Engine(player1, player2, board);

            Assert.AreEqual(true, engine.IsGameOver());
            Assert.AreEqual(null, engine.Winner);
        }

        [TestMethod]
        public void HorizontalWinTest()
        {
            const int size = 3;
            char[,] board = new char[size, size]
            {
                { 'X', 'X', 'X' },
                { 'X', 'O', 'O' },
                { 'O', ' ', 'O' }
            };

            IPlayer player1 = new RandomIA("AI_1", 'X', size);
            IPlayer player2 = new RandomIA("AI_2", 'O', size);
            IPlayer expected = player1;
            Engine engine = new Engine(player1, player2, board);

            Assert.AreEqual(true, engine.IsGameOver());
            Assert.AreEqual(player1, engine.Winner);
        }

        [TestMethod]
        public void VerticalWinTest()
        {
            const int size = 3;
            char[,] board = new char[size, size]
            {
                { 'X', 'X', 'O' },
                { 'X', ' ', 'O' },
                { 'O', ' ', 'O' }
            };

            IPlayer player1 = new RandomIA("AI_1", 'X', size);
            IPlayer player2 = new RandomIA("AI_2", 'O', size);
            IPlayer expected = player2;
            Engine engine = new Engine(player1, player2, board);

            Assert.AreEqual(true, engine.IsGameOver());
            Assert.AreEqual(player2, engine.Winner);
        }

        [TestMethod]
        public void DiagonalWinTest1()
        {
            const int size = 3;
            char[,] board = new char[size, size]
            {
                { 'X', 'O', 'X' },
                { 'O', 'X', 'O' },
                { 'O', ' ', 'X' }
            };

            IPlayer player1 = new RandomIA("AI_1", 'X', size);
            IPlayer player2 = new RandomIA("AI_2", 'O', size);
            IPlayer expected = player1;
            Engine engine = new Engine(player1, player2, board);

            Assert.AreEqual(true, engine.IsGameOver());
            Assert.AreEqual(player1, engine.Winner);
        }

        [TestMethod]
        public void DiagonalWinTest2()
        {
            const int size = 3;
            char[,] board = new char[size, size]
            {
                { 'O', 'O', 'X' },
                { 'O', 'X', 'O' },
                { 'X', ' ', 'X' }
            };

            IPlayer player1 = new RandomIA("AI_1", 'X', size);
            IPlayer player2 = new RandomIA("AI_2", 'O', size);
            IPlayer expected = player1;
            Engine engine = new Engine(player1, player2, board);

            Assert.AreEqual(true, engine.IsGameOver());
            Assert.AreEqual(player1, engine.Winner);
        }
    }
}
