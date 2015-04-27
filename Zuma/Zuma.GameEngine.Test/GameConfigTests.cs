using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zuma.GameEngine.Test
{
    [TestClass]
    public class GameConfigTests
    {
        [TestMethod]
        public void LoadConfig()
        {
            GameConfig config = new GameConfig(@"E:\ZumaGame\zumagame\Zuma\Zuma.DesktopUI\Map01.map");


            Assert.AreEqual(new Size(400, 400), config.Field.Size);
            
            Assert.AreEqual(new PointF(343, 18), config.BonusLocations[1]);
            Assert.AreEqual(3, config.BonusLocations.Length);
            
            Assert.AreEqual(2500, config.Path.Points.Length);
            Assert.AreEqual(67, ((BezierCombinedPath)config.Path).PointsSVG.Length);
        }
    }
}
