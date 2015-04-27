using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zuma.GameEngine.Test
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void BallCreation()
        {
            Ball ball = new Ball() { Type = BallType.Normal, Color = BallColor.Blue };

            Assert.AreEqual(BallType.Normal, ball.Type);
            Assert.AreEqual(BallColor.Blue, ball.Color);
        }

        [TestMethod]
        public void ChangeBallCoordinates()
        {
            Ball ball = new Ball() { Centre = new PointF(10, 10) };

            ball.Centre = new PointF(20, 20);

            Assert.AreEqual(new PointF(20, 20), ball.Centre);
        }
    }
}
