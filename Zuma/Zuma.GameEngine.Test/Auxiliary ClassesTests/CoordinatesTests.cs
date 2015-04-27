using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zuma.GameEngine.Test
{
    [TestClass]
    public class CoordinatesTests
    {
        [TestMethod]
        public void PointsMultiplying()
        {
            System.Drawing.PointF p1 = new System.Drawing.PointF(2.0F, 2.0F);
            
            System.Drawing.PointF pMul = new System.Drawing.PointF(6.0F, 6.0F);


            Assert.AreEqual(pMul, p1.Multiply(3.0F));
        }

        [TestMethod]
        public void PointsAdding()
        {
            System.Drawing.PointF p1 = new System.Drawing.PointF(2.0F, 2.0F);
            System.Drawing.PointF p2 = new System.Drawing.PointF(3.0F, 3.0F);

            System.Drawing.PointF pAdd = new System.Drawing.PointF(5.0F, 5.0F);


            Assert.AreEqual(pAdd, p1.Add(p2));
        }
    }
}
