using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _02_GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            //BoardPrinter printer = new ConsolePrinter();
            BoardPrinter printer = new CursesConsolePrinter();
            printer.Init();

            //int width = 20;
            //int height = 20;
            //Board board = BoardGenerator.CreateRandomStateBoard(width, height);

            Board board = BoardGenerator.LoadFromFile(@"gosperGliderGun.txt");
            printer.Print(board);

            Simulation simulation = new Simulation(board);

            while (true)
            {
                Thread.Sleep(50);
                printer.Print(simulation.NextState());
            }
        }
    }
}
