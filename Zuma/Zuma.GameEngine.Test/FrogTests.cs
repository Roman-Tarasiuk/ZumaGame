using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zuma.GameEngine.Test
{
    [TestClass]
    public class FrogTests
    {
        [TestMethod]
        public void FrogAngle()
        {
            Frog frog = new Frog();
            frog.Angle = new Angle() { Degree = 389.18 };

            Assert.AreEqual(29.18F, frog.Angle);
        }
    }
}
