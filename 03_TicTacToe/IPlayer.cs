using System;

namespace _03_TicTacToe
{
    public abstract class IPlayer
    {
        public string Name { get; protected set; }
        internal char Symbol { get; private set; }
        internal IPlayer(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        internal abstract Tuple<int, int> AskNextMove(char[,] board = null);
    }
}