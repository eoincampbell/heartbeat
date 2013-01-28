using System.Linq;
using HeartBeat.Logic.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeartBeat.Tests
{
    [TestClass]
    public class HeartBeatSettingsTests
    {
        private const string ThisServer = "TestServerName";

        [TestMethod]
        public void HeartBeatSettings_Load_On_App_Start_Should_Not_Throw_Exception()
        {
            var actual = HeartBeatSettings.SettingsManager.ThisServer;

            Assert.AreEqual(ThisServer, actual);
        }

        [TestMethod]
        public void HeartBeatSettings_Load_On_App_Start_Should_Have_2_SqlQueries()
        {
            const int expected = 2;
            var actual = HeartBeatSettings.SettingsManager.Queries.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HeartBeatSettings_Load_On_App_Start_Should_Have_3_Servers()
        {
            const int expected = 3;
            var actual = HeartBeatSettings.SettingsManager.Servers.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HeartBeatSettings_Load_On_App_Start_Should_Have_1_Service()
        {
            const int expected = 1;
            var actual = HeartBeatSettings.SettingsManager.Services.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HeartBeatSettings_Load_On_App_Start_Should_Have_3_URIs()
        {
            const int expected = 3;
            var actual = HeartBeatSettings.SettingsManager.URIs.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HeartBeatSettings_Load_On_App_Start_Should_Have_2_Drive()
        {
            const int expected = 2;
            var actual = HeartBeatSettings.SettingsManager.Drives.Count();

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void HeartBeatSettings_Load_On_App_Start_Should_Have_2_Powershell_Script_Checks()
        {
            const int expected = 2;
            var actual = HeartBeatSettings.SettingsManager.PowershellScripts.Count();

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void HeartBeatSettings_Load_On_App_Start_Should_Have_1_TaskScheduler_Check()
        {
            const int expected = 2;
            var actual = HeartBeatSettings.SettingsManager.Drives.Count();

            Assert.AreEqual(expected, actual);
        }
    }
}
