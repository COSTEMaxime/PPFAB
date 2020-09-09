using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_TicTacToe
{
    internal class MinimaxIA : WinningMoveIA
    {
        public MinimaxIA(string name, char symbol, int boardSize) : base(name, symbol, boardSize)
        {
        }

        internal override Tuple<int, int> AskNextMove(char[,] board)
        {
            Tuple<int, int> bestMove = null;
            int bestScore = int.MinValue;

            foreach (var move in BoardUtils.GetAllLegalMoves(board, Engine.EMPTY_CHAR))
            {
                char[,] workingBoard = (char[,])board.Clone();
                workingBoard[move.Item1, move.Item2] = Symbol;
                IPlayer opponent = PlayerManager.GetInstance().GetOpponent(Symbol);
                int currentScore  = minimaxScore(workingBoard, opponent.Symbol);

                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                    bestMove = new Tuple<int, int>(move.Item1, move.Item2);
                }
            }

            return bestMove;
        }

        private int minimaxScore(char[,] board, char currentSymbol)
        {
            Tuple<bool, char> result = BoardUtils.ComputeGameResult(board, Engine.EMPTY_CHAR);
            if (result.Item1)
            {
                // win
                if (result.Item2 == Symbol) { return 10; }
                // loose
                else if (result.Item2 != Engine.EMPTY_CHAR) { return -10; }
                // draw
                else { return 0; }
            }

            List<int> scores = new List<int>();
            foreach (var move in BoardUtils.GetAllLegalMoves(board, Engine.EMPTY_CHAR))
            {
                char[,] workingBoard = (char[,])board.Clone();
                workingBoard[move.Item1, move.Item2] = currentSymbol;
                IPlayer opponent = PlayerManager.GetInstance().GetOpponent(currentSymbol);
                scores.Add(minimaxScore(workingBoard, opponent.Symbol));
            }

            // if current player is our IA, we try to maximize the score,
            // else minimize it
            if (currentSymbol == Symbol)
            {
                return scores.Max();
            }
            else
            {
                return scores.Min();
            }
        }
    }
}
