using System;

namespace _03_TicTacToe
{
    public class HumanPlayer : IPlayer
    {
        public HumanPlayer(string name, char symbol) : base (name, symbol)
        {
            Console.Write("Enter " + name  + " name : ");
            Name = Console.ReadLine();
        }

        internal override Tuple<int, int> AskNextMove(char[,] board = null)
        {
            Console.Write("Player " + Name + " move. X coordinate : ");
            int x = safeUserInput();
            Console.Write("Player " + Name + " move. Y coordinate : ");
            int y = safeUserInput();
            return new Tuple<int, int>(x, y);
        }

        private int safeUserInput()
        {
            int? input = null;
            while (input == null)
            {
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e) when (e is FormatException || e is OverflowException)
                {
                    Console.WriteLine("Incorrect format. Try again");
                }
            }

            return (int)input;
        }
    }
}