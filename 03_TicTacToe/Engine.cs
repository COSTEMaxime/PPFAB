using System;
using System.Runtime.Remoting.Metadata;
using System.Text;

namespace _03_TicTacToe
{
    public class Engine
    {
        public IPlayer? Winner { get; private set; } = null;

        private char[,] board;
        private IPlayer[] players;
        private int currentPlayer;
        private const char EMPTY_CHAR = ' ';

        public Engine(IPlayer player1, IPlayer player2, int boardSize = 3)
        {
            players = new IPlayer[]
            {
                player1, player2
            };

            currentPlayer = 0;

            board = BoardGenerator.Generate(boardSize, EMPTY_CHAR);
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
        }

        public bool IsGameOver()
        {
            char c;
            bool win;

            // horizontal
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i, 0] == EMPTY_CHAR) { continue; }

                c = board[i, 0];
                win = true;
                for (int j = 1; j < board.GetLength(1); j++)
                {
                    if (c != board[i, j])
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

            // vertical
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[0, j] == EMPTY_CHAR) { continue; }

                win = true;
                c = board[0, j];
                for (int i = 1; i < board.GetLength(0); i++)
                {
                    if (c != board[i, j])
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

            // diagonals
            if (board[0, 0] == EMPTY_CHAR || 
                board[0, board.GetLength(0) - 1] == EMPTY_CHAR)
            {
                return false;
            }

            c = board[0, 0];
            win = true;
            for (int i = 1; i < board.GetLength(0); i++)
            {
                if (board[i, i] != c)
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

            c = board[0, board.GetLength(0) - 1];
            win = true;
            for (int i = 1; i < board.GetLength(0); i++)
            {
                if (board[i, board.GetLength(0) - 1 - i] != c)
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

        internal void NextMove()
        {
            Tuple<int, int> move = players[currentPlayer].AskNextMove();
            while (!checkPlayerMove(move))
            {
                Console.WriteLine("Invalid coordinates. Try again");
                move = players[currentPlayer].AskNextMove();
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