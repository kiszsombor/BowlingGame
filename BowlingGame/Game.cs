using System;

namespace BowlingGame
{
    public class Game
    {
        private readonly int[] rolls;
        private int currentRoll;
        private int frameCounter;

        public Game()
        {
            rolls = new int[21];
            currentRoll = 0;
            frameCounter = 0;

        }

        
        public int FrameCounter { get => frameCounter; }
        public int CurrentRoll { get => currentRoll; }

        public void Roll(int pins)
        {
            if (pins > 10)
            {
                throw new Exception("Cannot score more than 10 pins");
            }

            if (currentRoll > 20 || frameCounter > 22) 
            {
                throw new ArgumentOutOfRangeException("currentFrame", frameCounter, "Out of frame roll");
            }
            
            rolls[currentRoll] = pins;

            if (IsStrike(currentRoll))
            {
                ++frameCounter;
            }

            ++frameCounter;
            ++currentRoll;
        }

        private bool IsSpare(int frameIndex)
        {
            return rolls[frameIndex] + rolls[frameIndex + 1] == 10;
        }

        private bool IsStrike(int frameIndex)
        {
            return rolls[frameIndex] == 10;
        }

        private int SumOfBallsInFrame(int frameIndex)
        {
            return rolls[frameIndex] + rolls[frameIndex + 1];
        }

        private int SpareBonus(int frameIndex)
        {
            return rolls[frameIndex + 2];
        }

        private int StrikeBonus(int frameIndex)
        {
            return rolls[frameIndex + 1] + rolls[frameIndex + 2];
        }


        public int Score()
        {
            int score = 0;
            int frameIndex = 0;

            for(int frame = 0; frame < 10; ++frame)
            {
                if (IsStrike(frameIndex))
                {
                    score += 10 + StrikeBonus(frameIndex);
                    ++frameIndex;
                }
                else if (IsSpare(frameIndex))
                {
                    score += 10 + SpareBonus(frameIndex);
                    frameIndex += 2;
                }
                else
                {
                    score += SumOfBallsInFrame(frameIndex);
                    frameIndex += 2;
                }
            }

            return score;
        }
    }
}
