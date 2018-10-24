using System;
using InteroperatingUnmanagedCode.Managers;
using NUnit.Framework;

namespace InteroperatingUnmanagedCode
{
    [TestFixture]
    public class PowerManagerTests
    {
        [Test]
        public void SystemPowerInformationTest()
        {
            Console.WriteLine($"{string.Join("", PowerInfoManager.GetSystemPowerInformation())}");
        }

        [Test]
        public void LastSleepTimeTest()
        {
            SleepWakeTimeManager.GetLastSleepTime();
        }

        [Test]
        public void LastWakeTimeTest()
        {
            SleepWakeTimeManager.GetLastWakeTime();
        }

        [Test]
        public void SystemBatteryStateTest()
        {
            Console.WriteLine($"MaxCapacity: {BatteryManager.GetSystemBatteryState()}");
        }

        [Test]
        public void DeleteHybernationFileTest()
        {
            Assert.IsFalse(HybernateFileManager.ManageHybernateFile(false));
        }

        [Test]
        public void ReserveHybernationFileTest()
        {
            Assert.IsFalse(HybernateFileManager.ManageHybernateFile(true));
        }

        [Test]
        public void HybernateTest()
        {
            Assert.IsTrue(Hybernator.Hybernate());
        }

        [Test]
        public void SleepTest()
        {
            Assert.IsTrue(Hybernator.Sleep());
        }
    }
}
