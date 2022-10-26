using BowlingGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestBowlingGame
{
    [TestClass]
    public class TestBowlingMatch
    {
        private Match m;

        [TestInitialize]
        public void TestInitialize()
        {
            m = new Match();
        }

        [TestMethod]
        public void TestMatchWinner()
        {
            m.RollMany(m.Players[0], 19, 0);
            m.RollMany(m.Players[1], 19, 1);
            m.RollMany(m.Players[2], 19, 2);
            m.RollMany(m.Players[3], 19, 3);

            Assert.AreEqual(m.Players[3].Name, m.GetWinner().Name);
        }

        [TestMethod]
        public void TestMatchWinnerWithStrikesAndSpaers()
        {
            m.RollMany(m.Players[0], 19, 1);
            m.RollMany(m.Players[1], 19, 3);
            m.RollSpare(m.Players[2], 7, 3);
            m.RollMany(m.Players[2], 13, 2);
            m.RollStrike(m.Players[2]);
            m.RollStrike(m.Players[2]);
            m.RollSpare(m.Players[2], 5, 5);
            m.RollMany(m.Players[3], 19, 4);

            Assert.AreEqual(m.Players[2].Name, m.GetWinner().Name);
        }

        [TestMethod]
        public void TestMatchWinnerWithStrikesWithPerfectGame()
        {
            m.RollMany(m.Players[0], 19, 2);
            for (int i = 0; i < 12; ++i)
            {
                m.Roll(m.Players[1], 10);
            }
            m.RollStrike(m.Players[2]);
            m.RollMany(m.Players[2], 18, 3);
            m.RollMany(m.Players[3], 19, 4);

            Assert.AreEqual(m.Players[1].Name, m.GetWinner().Name);
        }

        [TestMethod]
        public void TestMatchNotFinished()
        {
            try
            {
                m.RollMany(m.Players[0], 18, 0);
                m.RollMany(m.Players[1], 18, 1);
                m.RollMany(m.Players[2], 18, 2);
                m.RollMany(m.Players[3], 18, 3);

                m.GetWinner();
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The game haven't fnished yet");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }
    }
}
