using System;
using System.Runtime.InteropServices;
using HeartBeat.Logic.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeartBeat.Tests
{
    [TestClass]
    public class DriveSpaceCheckTests
    {
        private const string TestName = "Test";

        //Servers
        private const string Localhost = "localhost";
        private const string ValidRemote = "192.168.8.135";
        private const string InvalidRemote = "192.168.8.7"; 
        private const string NonExistentRemote = "129.1.1.1";

        //Drive Letters
        private const string ValidDriveLetter = "C:";
        private const string InvalidDriveLetter = "Q:";

        //Thresholds
        private const double WithinThreshold= 0.03;
        private const double OutsideOfThreshold = 0.75;


        //Credentials
        private const string Domain = "WIN2K3TEST";
        private const string Username = "Administrator";
        private const string Password = "password";

        /*********************************************
        LOCAL TESTS
        *********************************************/
        [TestMethod]
        public void Check_Drive_Space_Available_Of_Local_Drive_Within_Threshold_Should_Return_True()
        {
            var actual = (new DriveSpaceHealthCheck(TestName, Localhost, ValidDriveLetter, WithinThreshold, string.Empty, string.Empty, string.Empty)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Check_Drive_Space_Available_Of_Local_Drive_With_Outside_Of_Threshold_Should_Return_False()
        {
            var actual = (new DriveSpaceHealthCheck(TestName, Localhost, ValidDriveLetter, OutsideOfThreshold, string.Empty, string.Empty, string.Empty)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_Drive_Space_Available_Of_Non_Existent_Local_Drive_Should_Throw_Argument_Exception()
        {
            (new DriveSpaceHealthCheck(TestName, Localhost, InvalidDriveLetter, WithinThreshold, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }

        /*********************************************
        VALID REMOTE SERVER TESTS
        *********************************************/
        [TestMethod]
        public void Check_Drive_Space_Available_Of_Remote_Drive_Outside_Of_Threshold_Should_Return_False()
        {
            var actual = (new DriveSpaceHealthCheck(TestName, ValidRemote, ValidDriveLetter, OutsideOfThreshold, string.Empty, string.Empty, string.Empty)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Check_Drive_Space_Available_Of_Remote_Drive_Within_Threshold_Should_Return_True()
        {
            var actual = (new DriveSpaceHealthCheck(TestName, ValidRemote, ValidDriveLetter, WithinThreshold, string.Empty, string.Empty, string.Empty)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_Drive_Space_Available_Of_Non_Existent_Remote_Drive_Should_Throw_Argument_Exception()
        {
            (new DriveSpaceHealthCheck(TestName, ValidRemote, InvalidDriveLetter, WithinThreshold, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }

        /*********************************************
        INVALID REMOTE SERVER TESTS - I.e. NO PERMISSION
        *********************************************/

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Check_Drive_Space_Available_Of_Remote_Drive_Outside_Of_Threshold_Without_Permission_Should_Throw_UnauthorizedAccess_Exception()
        {
            (new DriveSpaceHealthCheck(TestName, InvalidRemote, ValidDriveLetter, OutsideOfThreshold, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Check_Drive_Space_Available_Of_Remote_Drive_Within_Threshold_Without_Permission_Should_Throw_UnauthorizedAccess_Exception()
        {
            (new DriveSpaceHealthCheck(TestName, InvalidRemote, ValidDriveLetter, WithinThreshold, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }

        /**************************************************
        NON-EXISTENT / NON-ACCESSIBLE REMOTE SERVER TESTS
        **************************************************/

        [TestMethod]
        [ExpectedException(typeof(COMException))]
        public void Check_Drive_Space_Available_Of_Remote_Drive_On_Non_Existent_Server_Should_Throw_ComInterop_Exception()
        {
            (new DriveSpaceHealthCheck(TestName, NonExistentRemote, ValidDriveLetter, WithinThreshold, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }

        /**************************************************
        USERNAME & PASSWORD Checks
        **************************************************/
        [TestMethod]
        public void Check_Drive_Space_Available_Of_Remote_Drive_Within_Threshold_With_UserName_And_Password_Should_Return_True()
        {
            var actual = (new DriveSpaceHealthCheck(TestName, ValidRemote, ValidDriveLetter, WithinThreshold, Domain, Username, Password)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Check_Drive_Space_Available_Of_Remote_Drive_Within_Threshold_With_Incorrect_UserName_And_Password_Should_Throw_UnauthorizedAccess_Exception()
        {
            (new DriveSpaceHealthCheck(TestName, ValidRemote, ValidDriveLetter, WithinThreshold, Domain, Username, Password + "WRONG")).PerformCheck();
        }

        /**************************************************
        Bad Threshold Values
        **************************************************/
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_Drive_Space_Available_With_Bad_Threshold_Parameter_Below_Min_Drive_Should_Throw_Argument_Exception()
        {
            (new DriveSpaceHealthCheck(TestName, Localhost, ValidDriveLetter, 0, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_Drive_Space_Available_With_Bad_Threshold_Parameter_Above_Max_Drive_Should_Throw_Argument_Exception()
        {
            (new DriveSpaceHealthCheck(TestName, Localhost, ValidDriveLetter, 1, string.Empty, string.Empty, string.Empty)).PerformCheck();
        }
    }
}
