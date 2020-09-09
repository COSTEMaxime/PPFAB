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
        public static readonly char EMPTY_CHAR = ' ';

        public Engine(IPlayer player1, IPlayer player2, int boardSize = 3)
        {
            PlayerManager.GetInstance().SetPlayersAndResetCurrentPlayer(new IPlayer[]
            {
                player1, player2
            });

            board = BoardGenerator.Generate(boardSize, EMPTY_CHAR);
        }

        public Engine(IPlayer player1, IPlayer player2, char[,] board)
        {
            PlayerManager.GetInstance().SetPlayersAndResetCurrentPlayer(new IPlayer[]
            {
                player1, player2
            });

            if (board.GetLength(0) != board.GetLength(1))
            {
                throw new Exception("Board should be a square");
            }

            this.board = board;
        }

        public bool IsGameOver()
        {
            var gameResult = BoardUtils.ComputeGameResult(board, EMPTY_CHAR);
            if (gameResult.Item1 && gameResult.Item2 != EMPTY_CHAR)
            {
                Winner = getWinnerFromSymbol(gameResult.Item2);
            }

            return gameResult.Item1;
        }

        private IPlayer getWinnerFromSymbol(char c)
        {
            return PlayerManager.GetInstance().GetPlayerFromSymbol(c);
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
            Tuple<int, int> move = PlayerManager.GetInstance().GetCurrentPlayer().AskNextMove(board);
            while (!checkPlayerMove(move))
            {
                Console.WriteLine("Invalid coordinates. Try again");
                move = PlayerManager.GetInstance().GetCurrentPlayer().AskNextMove(board);
            }


            makeMove(move);

            PlayerManager.GetInstance().NextPlayer();
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

        private void makeMove(Tuple<int, int> move)
        {
            board[move.Item1, move.Item2] = PlayerManager.GetInstance().GetCurrentPlayer().Symbol;
        }
    }
}