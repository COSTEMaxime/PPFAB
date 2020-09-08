using _03_TicTacToe;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_TicTacToeTest
{
    [TestClass]
    public class IATest
    {
        [TestMethod]
        public void FindWinningMoveHorizontolTest()
        {
            const int size = 3;
            char[,] board = new char[size, size]
            {
                { 'X', ' ', 'X' },
                { 'X', 'O', ' ' },
                { 'O', ' ', 'O' }
            };

            IPlayer player1 = new WinningMoveIA("AI_1", 'X', size);
            IPlayer player2 = new WinningMoveIA("AI_2", 'O', size);
            Engine engine = new Engine(player1, player2, board);

            engine.NextMove();
            Assert.AreEqual(true, engine.IsGameOver());
            Assert.AreEqual(player1, engine.Winner);
        }

        [TestMethod]
        public void FindWinningMoveVerticalTest()
        {
            const int size = 3;
            char[,] board = new char[size, size]
            {
                { 'X', 'O', ' ' },
                { 'X', ' ', 'O' },
                { ' ', 'X', 'O' }
            };

            IPlayer player1 = new WinningMoveIA("AI_1", 'X', size);
            IPlayer player2 = new WinningMoveIA("AI_2", 'O', size);
            Engine engine = new Engine(player1, player2, board);

            engine.NextMove();
            Assert.AreEqual(true, engine.IsGameOver());
            Assert.AreEqual(player1, engine.Winner);
        }

        [TestMethod]
        public void FindWinningMoveDiagonalTest()
        {
            const int size = 3;
            char[,] board = new char[size, size]
            {
                { 'X', 'O', 'X' },
                { 'O', ' ', ' ' },
                { 'X', ' ', 'O' }
            };

            IPlayer player1 = new WinningMoveIA("AI_1", 'X', size);
            IPlayer player2 = new WinningMoveIA("AI_2", 'O', size);
            Engine engine = new Engine(player1, player2, board);

            engine.NextMove();
            Assert.AreEqual(true, engine.IsGameOver());
            Assert.AreEqual(player1, engine.Winner);
        }

        [TestMethod]
        public void BlockWinningMoveHorizontalTest()
        {
            const int size = 3;
            char[,] board = new char[size, size]
            {
                { 'X', ' ', 'X' },
                { 'X', 'O', ' ' },
                { 'O', ' ', ' ' }
            };

            IPlayer player1 = new SmartWinningMoveIA("AI_1", 'O', size);
            IPlayer player2 = new SmartWinningMoveIA("AI_2", 'X', size);
            Engine engine = new Engine(player1, player2, board);

            engine.NextMove();
            engine.NextMove();
            Assert.AreEqual(false, engine.IsGameOver());
        }

        [TestMethod]
        public void BlockWinningMoveVerticalTest()
        {
            const int size = 3;
            char[,] board = new char[size, size]
            {
                { 'X', 'O', ' ' },
                { 'X', ' ', ' ' },
                { ' ', 'X', 'O' }
            };

            IPlayer player1 = new SmartWinningMoveIA("AI_1", 'O', size);
            IPlayer player2 = new SmartWinningMoveIA("AI_2", 'X', size);
            Engine engine = new Engine(player1, player2, board);

            engine.NextMove();
            engine.NextMove();
            Assert.AreEqual(false, engine.IsGameOver());
        }

        [TestMethod]
        public void BlockWinningMoveDiagonalTest()
        {
            const int size = 3;
            char[,] board = new char[size, size]
            {
                { 'X', 'O', 'X' },
                { 'O', ' ', ' ' },
                { 'X', ' ', 'O' }
            };

            IPlayer player1 = new SmartWinningMoveIA("AI_1", 'O', size);
            IPlayer player2 = new SmartWinningMoveIA("AI_2", 'X', size);
            Engine engine = new Engine(player1, player2, board);

            engine.NextMove();
            engine.NextMove();
            Assert.AreEqual(false, engine.IsGameOver());
        }
    }
}
