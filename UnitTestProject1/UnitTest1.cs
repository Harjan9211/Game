using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Media;
using Shooting_Game;
using Shooting_Game.Properties;
using Mole_Shooter;
using Mole_Shooter.Properties;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SoundPlayer soundPlayer = new SoundPlayer();
            Assert.IsTrue(soundPlayer != null);
        }
        [TestMethod]
        public void TestMethod2()
        {
            SoundPlayer soundPlayer1 = new SoundPlayer();
            Assert.IsTrue(soundPlayer1 != null);
        }
        [TestMethod]
        public void TestMethod3()
        {
            SoundPlayer simpleSound = new SoundPlayer();
            Assert.IsTrue(simpleSound != null);
        }
    }
  
}
