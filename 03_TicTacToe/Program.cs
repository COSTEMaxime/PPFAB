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
            IPlayer player1 = new SmartWinningMoveIA("Player1", 'X', boardSize);
            IPlayer player2 = new WinningMoveIA("Player2", 'O', boardSize);

            repeatPlay(player1, player2, boardSize, 50);
            Console.ReadKey();
        }

        static void repeatPlay(IPlayer player1, IPlayer player2, int boardSize, int numberOfGames)
        {
            int draws = 0;
            int player1WinCount = 0;
            int player2WinCount = 0;

            for (int i = 0; i < numberOfGames; i++)
            {
                IPlayer? winner = play(player1, player2, boardSize, false);

                if (winner == null)
                {
                    draws++;
                }
                else if (winner == player1)
                {
                    player1WinCount++;
                }
                else
                {
                    player2WinCount++;
                }
            }

            Console.WriteLine("Draws : " + draws);
            Console.WriteLine("Player1 wins count : " + player1WinCount);
            Console.WriteLine("Player2 wins count : " + player2WinCount);
        }

        static IPlayer? play(IPlayer player1, IPlayer player2, int boardSize, bool render = true)
        {
            Engine engine = new Engine(player1, player2, boardSize);
            if (render) { engine.Render(); }

            while (!engine.IsGameOver())
            {
                engine.NextMove();
                if (render) { engine.Render(); }
            }

            return engine.Winner;
        }
    }
}
