using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_TicTacToe
{
    class PlayerManager
    {
        private static PlayerManager instance = null;
        private IPlayer[] players;
        private int currentPlayerIndex = 0;

        private PlayerManager() {}

        public static PlayerManager GetInstance()
        {
            if (instance == null)
            {
                instance = new PlayerManager();
            }

            return instance;
        }

        public void SetPlayersAndResetCurrentPlayer(IPlayer[] players)
        {
            this.players = players;
            this.currentPlayerIndex = 0;
        }

        public IPlayer GetPlayerFromSymbol(char c)
        {
            foreach (IPlayer player in players)
            {
                if (player.Symbol == c) { return player; }
            }

            throw new Exception("Could not find player with char '" + c + "'");
        }

        internal IPlayer GetOpponent(char c)
        {
            foreach (IPlayer player in players)
            {
                if (player.Symbol != c) { return player; }
            }

            throw new Exception("Could not find an oponent to player with char '" + c + "'");
        }

        internal void NextPlayer()
        {
            currentPlayerIndex = ++currentPlayerIndex % players.Length;
        }

        internal IPlayer GetCurrentPlayer()
        {
            return players[currentPlayerIndex];
        }
    }
}
