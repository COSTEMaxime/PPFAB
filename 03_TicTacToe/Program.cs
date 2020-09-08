using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            const int boardSize = 3;
            IPlayer player1 = new WinningMoveIA("Player1", 'X', boardSize);
            IPlayer player2 = new WinningMoveIA("Player2", 'O', boardSize);
            Engine engine = new Engine(player1, player2, boardSize);
            engine.Render();

            while (!engine.IsGameOver())
            {
                engine.NextMove();
                engine.Render();
            }

            if (engine.Winner == null)
            {
                Console.WriteLine("Draw");
            }
            else
            {
                Console.WriteLine(engine.Winner);
            }

            Console.ReadKey();
        }
    }
}
