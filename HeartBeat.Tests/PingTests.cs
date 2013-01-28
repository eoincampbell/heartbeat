using HeartBeat.Logic.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeartBeat.Tests
{
    [TestClass]
    public class PingTests
    {
        private const string ValidHost = "www.google.com";
        private const string InvalidHost = "nonexistent";
        private const string FriendlyName = "friendly name";
        

        [TestMethod]
        public void Pinging_Accessible_Server_Returns_True()
        {
            const bool expected = true;
            var actual =  (new PingHealthCheck(FriendlyName, ValidHost, 2000)).PerformCheck();

            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void Pinging_Inaccessible_Server_Returns_False()
        {
            var actual = (new PingHealthCheck(FriendlyName, InvalidHost, 2000)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Pinging_Inaccessible_Server_With_Very_Small_Timeout_Returns_False()
        {
            var actual = (new PingHealthCheck(FriendlyName, InvalidHost, 1)).PerformCheck();

            Assert.IsFalse(actual);
        }
    }
}
