using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame
{
    public class Player
    {
        private readonly Game game;
        private bool isFinished;

        public Player(string name)
        {
            Name = name;
            isFinished = false;
            game = new Game();
        }

        public string Name { get; }
        public bool IsFinished { get => isFinished; }
        public Game Game { get => game; }

        private void CheckFinished()
        {
            if (game.CurrentRoll > 18 || game.FrameCounter > 22)
            {
                isFinished = true;
            }
        }

        public void Roll(int pins)
        {
            if (isFinished)
            {
                throw new Exception("The game has already finished");
            }
            game.Roll(pins);
            CheckFinished();
        }

        public int GetScore()
        {
            return game.Score();
        }

        public void RollMany(int n, int pins)
        {
            if (pins > 10)
            {
                throw new Exception("Cannot score more than 10 pins");
            }

            for (int i = 0; i < n; ++i)
            {
                if (isFinished)
                {
                    throw new Exception("The game has already finished");
                }
                game.Roll(pins);
                CheckFinished();
            }
        }

        public void RollSpare(int roll1, int roll2)
        {
            if (isFinished)
            {
                throw new Exception("The game has already finished");
            }
            
            if (roll1 + roll2 != 10)
            {
                throw new Exception("roll1 pins + roll2 pins has to be 10");
            }

            game.Roll(roll1);
            game.Roll(roll2);

            CheckFinished();
        }

        public void RollStrike()
        {
            if (isFinished)
            {
                throw new Exception("The game has already finished");
            }

            game.Roll(10);

            CheckFinished();
        }
    }
}
