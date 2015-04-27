using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zuma.GameEngine.Test
{
    [TestClass]
    public class BallSequenceTests
    {
        [TestMethod]
        public void AddBallsToSequence()
        {
            BallSequence sequence = new BallSequence();
            sequence.Balls.Add(new Ball());
            sequence.Balls.Add(new Ball());
            
            Assert.AreEqual(2, sequence.Balls.Count);
        }
    }
}
