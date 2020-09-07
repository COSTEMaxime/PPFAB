using _02_GameOfLife;
using NUnit.Framework;

namespace NUnitTestProject1
{
    public class BoardGeneratorTests
    {

        [Test]
        public void DeadBoardGeneratorTest()
        {
            const int width = 5;
            const int height = 2;

            Board expected = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, false, false, false, false }
                }
            };

            Assert.AreEqual(expected, BoardGenerator.CreateDeadStateBoard(width, height));
        }

        [Test]
        public void AliveBoardGeneratorTest()
        {
            const int width = 5;
            const int height = 2;

            Board expected = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { true, true, true, true, true },
            { true, true, true, true, true }
                }
            };

            Assert.AreEqual(expected, BoardGenerator.CreateAliveStateBoard(width, height));
        }

        [Test]
        public void StableTest()
        {
            const int width = 5;
            const int height = 5;

            Board board = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, false, true, false, false },
                { false, true, false, true, false },
                { false, false, true, false, false },
                { false, false, false, false, false }
                }
            };

            Board expected = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, false, true, false, false },
                { false, true, false, true, false },
                { false, false, true, false, false },
                { false, false, false, false, false }
                }
            };

            Simulation simulation = new Simulation(board);
            simulation.NextState();
            Assert.AreEqual(expected, simulation.NextState());
        }

        [Test]
        public void OverpopulationTest()
        {
            const int width = 5;
            const int height = 5;

            Board board = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, true, false, true, false },
                { false, false, true, false, false },
                { false, true, false, true, false },
                { false, false, false, false, false }
                }
            };

            Board expected = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, false, true, false, false },
                { false, true, false, true, false },
                { false, false, true, false, false },
                { false, false, false, false, false }
                }
            };

            Simulation simulation = new Simulation(board);
            Assert.AreEqual(expected, simulation.NextState());
        }

        [Test]
        public void UnderpopulationTest()
        {
            const int width = 5;
            const int height = 5;

            Board board = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, false, true, false, false },
                { false, false, true, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }
                }
            };

            Board expected = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }
                }
            };

            Simulation simulation = new Simulation(board);
            Assert.AreEqual(expected, simulation.NextState());
        }

        [Test]
        public void NewCellTest()
        {
            const int width = 5;
            const int height = 5;

            Board board = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, false, true, false, false },
                { false, true, false, true, false },
                { false, false, false, false, false },
                { false, false, false, false, false }
                }
            };

            Board expected = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, false, true, false, false },
                { false, false, true, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }
                }
            };

            Simulation simulation = new Simulation(board);
            Assert.AreEqual(expected, simulation.NextState());
        }

        [Test]
        public void NewCellAndOverpopulationTest()
        {
            const int width = 5;
            const int height = 5;

            Board board = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, true, true, true, false },
                { false, true, true, true, false },
                { false, true, true, true, false },
                { false, false, false, false, false }
                }
            };

            Board expected = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, true, false, false },
                { false, true, false, true, false },
                { true, false, false, false, true },
                { false, true, false, true, false },
                { false, false, true, false, false }
                }
            };

            Simulation simulation = new Simulation(board);
            Assert.AreEqual(expected, simulation.NextState());
        }

        [Test]
        public void GliderTest()
        {
            const int width = 5;
            const int height = 5;

            Board board = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, false, true, false, false },
                { false, false, false, true, false },
                { false, true, true, true, false },
                { false, false, false, false, false }
                }
            };

            Board expected = new Board
            {
                width = width,
                height = height,
                board = new bool[height, width]
                {
                { false, false, false, false, false },
                { false, false, false, false, false },
                { false, false, true, false, false },
                { false, false, false, true, true },
                { false, false, true, true, false }
                }
            };

            Simulation simulation = new Simulation(board);
            simulation.NextState();
            simulation.NextState();
            Assert.AreEqual(expected, simulation.NextState());
        }
    }
}