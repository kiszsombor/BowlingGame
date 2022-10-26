using BowlingGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestBowlingGame
{
    [TestClass]
    public class TestBowlingPlayer
    {
        private Player p;

        [TestInitialize]
        public void TestInitialize()
        {
            p = new Player("player");
        }

        [TestMethod]
        public void TestPlayerOneRoll()
        {
            p.Roll(3);

            Assert.AreEqual(3, p.GetScore());
        }

        [TestMethod]
        public void TestPlayerOneSpare()
        {
            p.RollSpare(4, 6);
            p.Roll(3);
            p.Roll(3);

            Assert.AreEqual(19, p.GetScore());
        }

        [TestMethod]
        public void TestPlayerOneSpareError()
        {
            try
            {
                p.RollSpare(4, 4);
                p.Roll(3);
                p.Roll(3);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "roll1 pins + roll2 pins has to be 10");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void TestPlayerOneStrike()
        {
            p.RollStrike();
            p.Roll(3);
            p.Roll(3);

            Assert.AreEqual(22, p.GetScore());
        }

        [TestMethod]
        public void TestPlayerGameFinished()
        {
            p.RollMany(4, 3);
            p.RollStrike();
            p.RollStrike();
            p.RollMany(8, 4);
            p.RollSpare(1, 9);
            p.RollSpare(5, 5);
            p.Roll(7);
            Assert.IsTrue(p.IsFinished);
        }

        [TestMethod]
        public void TestPlayerGameFinishedPerfectly()
        {
            for(int i = 0; i < 12; ++i)
            {
                p.RollStrike();
            }
            Assert.IsTrue(p.IsFinished);
        }

        [TestMethod]
        public void TestPlayerRollTooMany()
        {
            try
            {
                p.RollMany(22, 3);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The game has already finished");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void TestPlayerRollAfterGameFinished()
        {
            try
            {
                p.RollMany(21, 3);
                p.Roll(7);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The game has already finished");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }
        [TestMethod]
        public void TestPlayerRollSpareAfterGameFinished()
        {
            try
            {
                p.RollMany(21, 3);
                p.RollSpare(8, 2);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The game has already finished");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void TestPlayerRollStrikeAfterGameFinished()
        {
            try
            {
                p.RollMany(21, 3);
                p.RollStrike();
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "The game has already finished");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        
    }
}
