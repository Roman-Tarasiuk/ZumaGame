using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zuma.GameEngine.Test
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GameStatusOnCreation()
        {
            Game game = new Game();

            Assert.AreEqual(GameStatus.Created, game.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void OnlyInitializedGameCanPlay()
        {
            Game game = new Game();

            game.Play();
        }
    }
}
