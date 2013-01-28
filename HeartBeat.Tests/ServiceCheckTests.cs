using System;
using System.Runtime.InteropServices;
using HeartBeat.Logic.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeartBeat.Tests
{
    [TestClass]
    public class ServiceCheckTests
    {
        private const string TestName = "Test";

        //Servers
        private const string Localhost = "localhost";
        private const string ValidRemote = "192.168.8.135"; //WIN2K3TEST
        private const string InvalidRemote = "192.168.8.7"; //Giovanni's Laptop
        private const string NonExistentRemote = "129.1.1.1";

        //Services
        private const string RunningService = "W3SVC";
        private const string NonRunningService = "ClipSrv";
        private const string NonRunningService2 = "Fax";
        private const string NonExistentService = "NOTREALSERVICE";

        //Credentials
        private const string Domain = "WIN2K3TEST";
        private const string Username = "Administrator";
        private const string Password = "password";

        /*********************************************
        LOCAL TESTS
        *********************************************/
        [TestMethod]
        public void Check_Service_Status_Of_Non_Running_Local_Service_Should_Return_False()
        {
            var actual = (new ServiceHealthCheck(TestName, Localhost, NonRunningService2, string.Empty, string.Empty, string.Empty)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Check_Service_Status_Of_Running_Local_Service_Should_Return_True()
        {
            var actual = (new ServiceHealthCheck(TestName, Localhost, RunningService, string.Empty, string.Empty, string.Empty)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_Service_Status_Of_Non_Existent_Local_Service_Should_Throw_Argument_Exception()
        {
            (new ServiceHealthCheck(TestName, Localhost, NonExistentService, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }

        /*********************************************
        VALID REMOTE SERVER TESTS
        *********************************************/
        [TestMethod]
        public void Check_Service_Status_Of_Non_Running_Remote_Service_Should_Return_False()
        {
            var actual = (new ServiceHealthCheck(TestName, ValidRemote, NonRunningService, string.Empty, string.Empty, string.Empty)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Check_Service_Status_Of_Running_Remote_Service_Should_Return_True()
        {
            var actual = (new ServiceHealthCheck(TestName, ValidRemote, RunningService, string.Empty, string.Empty, string.Empty)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_Service_Status_Of_Non_Existent_Remote_Service_Should_Throw_Argument_Exception()
        {
            (new ServiceHealthCheck(TestName, ValidRemote, NonExistentService, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }

        /*********************************************
        INVALID REMOTE SERVER TESTS - I.e. NO PERMISSION
        *********************************************/

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Check_Service_Status_Of_Non_Running_Remote_Service_Without_Permission_Should_Throw_UnauthorizedAccess_Exception()
        {
            (new ServiceHealthCheck(TestName, InvalidRemote, NonRunningService, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Check_Service_Status_Of_Running_Remote_Service_Without_Permission_Should_Throw_UnauthorizedAccess_Exception()
        {
            (new ServiceHealthCheck(TestName, InvalidRemote, RunningService, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }

        /**************************************************
        NON-EXISTENT / NON-ACCESSIBLE REMOTE SERVER TESTS
        **************************************************/
       
        [TestMethod]
        [ExpectedException(typeof(COMException))]
        public void Check_Service_Status_Of_Unknown_Remote_Service_On_Non_Existent_Server_Should_Throw_ComInterop_Exception()
        {
            (new ServiceHealthCheck(TestName, NonExistentRemote, NonExistentService, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }

        /**************************************************
        USERNAME & PASSWORD Checks
        **************************************************/
        [TestMethod]
        public void Check_Service_Status_Of_Running_Remote_Service_With_UserName_And_Password_Should_Return_True()
        {
            var actual = (new ServiceHealthCheck(TestName, ValidRemote, RunningService, Domain, Username, Password)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Check_Service_Status_Of_Running_Remote_Service_With_Incorrect_UserName_And_Password_Should_Throw_UnauthorizedAccess_Exception()
        {
            (new ServiceHealthCheck(TestName, ValidRemote, RunningService, Domain, Username, Password + "WRONG")).PerformCheck();
        }
    }
}
