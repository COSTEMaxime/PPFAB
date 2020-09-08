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
            IPlayer player1 = new HumanPlayer("Player1", 'X');
            IPlayer player2 = new HumanPlayer("Player2", 'O');
            Engine engine = new Engine(player1, player2);
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
