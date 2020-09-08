using System;
using System.Linq;
using System.Runtime.Remoting.Metadata;
using System.Text;

namespace _03_TicTacToe
{
    public class Engine
    {
        public IPlayer? Winner { get; private set; } = null;

        private char[,] board;
        private readonly int boardSize;
        private IPlayer[] players;
        private int currentPlayer;
        public static readonly char EMPTY_CHAR = ' ';

        public Engine(IPlayer player1, IPlayer player2, int boardSize = 3)
        {
            players = new IPlayer[]
            {
                player1, player2
            };

            currentPlayer = 0;

            board = BoardGenerator.Generate(boardSize, EMPTY_CHAR);
            this.boardSize = boardSize;
        }

        public Engine(IPlayer player1, IPlayer player2, char[,] board)
        {
            players = new IPlayer[]
            {
                player1, player2
            };

            currentPlayer = 0;

            if (board.GetLength(0) != board.GetLength(1))
            {
                throw new Exception("Board should be a square");
            }

            this.board = board;
            this.boardSize = board.GetLength(0);
        }

        public bool IsGameOver()
        {
            var boardLines = BoardGenerator.GetAllBoardLinesCoordinates(boardSize);

            foreach (var line in boardLines)
            {
                char c = board[line.First().Item1, line.First().Item2];
                if (c == EMPTY_CHAR) { continue; }

                bool win = true;
                foreach (var coord in line)
                {
                    if (c != board[coord.Item1, coord.Item2])
                    {
                        win = false;
                        break;
                    }
                }

                if (win)
                {
                    Winner = getWinnerFromSymbol(c);
                    return true;
                }
            }

            if (IsDraw())
            {
                Winner = null;
                return true;
            }

            return false;
        }

        private IPlayer getWinnerFromSymbol(char c)
        {
            foreach (IPlayer player in players)
            {
                if (player.Symbol == c) { return player; }
            }

            throw new Exception("Could not find player with char '" + c + "'");
        }

        internal void Render()
        {
            Console.WriteLine(" -0 1 2 -");
            Console.WriteLine(" --------");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(i);
                builder.Append('|');
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    builder.Append(board[i, j]);
                    builder.Append(EMPTY_CHAR);
                }
                builder.Append('|');
                Console.WriteLine(builder.ToString());
            }
            Console.WriteLine("------");
        }

        public void NextMove()
        {
            Tuple<int, int> move = players[currentPlayer].AskNextMove(board);
            while (!checkPlayerMove(move))
            {
                Console.WriteLine("Invalid coordinates. Try again");
                move = players[currentPlayer].AskNextMove(board);
            }


            makeMove(move);
            currentPlayer = ++currentPlayer % players.Length;
        }

        private bool checkPlayerMove(Tuple<int, int> move)
        {
            if (move.Item1 < 0 || move.Item1 >= board.GetLength(0) ||
                move.Item2 < 0 || move.Item2 >= board.GetLength(1))
            {
                return false;
            }

            return board[move.Item1, move.Item2] == EMPTY_CHAR;
        }

        internal bool IsDraw()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == EMPTY_CHAR) { return false; }
                }
            }

            return true;
        }

        private void makeMove(Tuple<int, int> move)
        {
            board[move.Item1, move.Item2] = players[currentPlayer].Symbol;
        }
    }
}