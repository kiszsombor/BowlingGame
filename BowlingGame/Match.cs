using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame
{
    public class Match
    {
        private readonly List<Player> players;

        public Match()
        {
            players = new List<Player>
            {
                new Player("Player1"),
                new Player("Player2"),
                new Player("Player3"),
                new Player("Player4")
            };
        }

        public List<Player> Players { get => players; }

        static void Main()
        {

        }

        public void RollStrike(Player player)
        {
            if (player.IsFinished)
            {
                throw new Exception("The player has already finished the game");
            }

            Player p = players.Find(i => i.Name.Equals(player.Name));
            p.RollStrike();
        }

        public void RollSpare(Player player, int roll1, int roll2)
        {
            if (player.IsFinished)
            {
                throw new Exception("The player has already finished the game");
            }
            if (roll1 + roll2 != 10)
            {
                throw new Exception("roll1 pins + roll2 pins has to be 10");
            }
            Player p = players.Find(i => i.Name.Equals(player.Name));
            p.RollSpare(roll1, roll2);
        }

        public void RollMany(Player player, int rollsCount, int pins)
        {
            if (player.IsFinished)
            {
                throw new Exception("The player has already finished the game");
            }
            if (pins > 10)
            {
                throw new Exception("A standard roll has to kock less then 10 pins");
            }
            Player p = players.Find(i => i.Name.Equals(player.Name));
            p.RollMany(rollsCount, pins);
        }

        public void Roll(Player player, int pins)
        {
            if (player.IsFinished)
            {
                throw new Exception("The player has already finished the game");
            }
            if (pins > 10)
            {
                throw new Exception("A standard roll has to kock less then 10 pins");
            }
            Player p = players.Find(i => i.Name.Equals(player.Name));
            p.Roll(pins);
        }

        public Player GetWinner()
        {
            if (!players.TrueForAll(i => i.IsFinished))
            {
                throw new Exception("The game haven't fnished yet");
            }

            Player winner = players[0];
            foreach (Player player in players)
            {
                if(winner.GetScore() < player.GetScore())
                {
                    winner = player;
                }
                else if (player != winner && winner.GetScore() == player.GetScore())
                {
                    throw new Exception("There is equals scores");
                }
            }

            return winner;
        }
    }
}
