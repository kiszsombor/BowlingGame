using BowlingGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestBowlingGame
{
    [TestClass]
    public class TestBowlingGame
    {
        private Game g;

        [TestInitialize]
        public void TestInitialize()
        {
            g = new Game();
        }

        private void RollMany(int n, int pins)
        {
            if(pins > 10)
            {
                throw new Exception("Cannot score more than 10 pins");
            }

            for (int i = 0; i < n; ++i)
            {
                g.Roll(pins);
            }
        }

        [TestMethod]
        public void TestGutterGame()
        {
            RollMany(20, 0);

            Assert.AreEqual(0, g.Score());
        }

        [TestMethod]
        public void TestAllOnes()
        {
            RollMany(20, 1);

            Assert.AreEqual(20, g.Score());
        }

        private void RollSpare()
        {
            g.Roll(5);
            g.Roll(5);
        }

        private void RollStrike()
        {
            g.Roll(10);
        }

        [TestMethod]
        public void TestOneSpare()
        {
            RollSpare();
            g.Roll(3);
            RollMany(17, 0);
            Assert.AreEqual(16, g.Score());
        }

        [TestMethod]
        public void TestOneStrike()
        {
            RollStrike();
            g.Roll(3);
            g.Roll(4);
            RollMany(16, 0);
            Assert.AreEqual(24, g.Score());
        }

        [TestMethod]
        public void TestPerfectGame()
        {
            RollMany(12, 10);
            Assert.AreEqual(300, g.Score());
        }

        [TestMethod]
        public void TestAllFivesGameScoreShouldBe150()
        {
            RollMany(21, 5);

            Assert.AreEqual(150, g.Score());
        }

        [TestMethod]
        public void TestTenPinsNotSpare()
        {
            g.Roll(0);
            g.Roll(7);
            g.Roll(3);
            g.Roll(2);
            RollMany(16, 0);
            Assert.AreEqual(12, g.Score());
        }

        [TestMethod]
        public void TestRollsOutOfFrameWhenLastRollStrike()
        {
            try
            {
                RollMany(19, 1);
                g.Roll(10);
                g.Roll(10);
                g.Roll(10);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "Out of frame roll");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void TestRollsOutOfFrame()
        {
            try
            {
                RollMany(22, 1);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "Out of frame roll");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void TestRollsOutOfFrameWithStrikes()
        {
            try
            {
                RollMany(13, 10);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "Out of frame roll");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void TestRollsOutOfFrameWithSpares()
        {
            try
            {
                RollMany(22, 5);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "Out of frame roll");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }
    }
}
