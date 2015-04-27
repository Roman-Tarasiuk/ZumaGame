using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zuma.GameEngine.Test.Auxiliary_ClassesTests
{
    [TestClass]
    public class AngleTests
    {
        [TestMethod]
        public void TestAngles1()
        {
            Angle angle = new Angle() { Degree = 225 };

            Assert.AreEqual(new Angle() { Radian = -3 * Math.PI / 4 }, angle);
        }

        [TestMethod]
        public void TestAngles2()
        {
            Angle angle = new Angle(5, 5);

            Assert.AreEqual(new Angle() { Radian = Math.PI / 4 }, angle);
        }
    }
}
