
using System.Management.Automation;
using HeartBeat.Logic.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeartBeat.Tests
{
    [TestClass]
    public class PowershellTests
    {
        private const string TestName = "Test";

        private const string PathSuccessTest = @"D:\Work\Github\heartBeat\HeartBeat.Tests\Scripts\ShouldSucceed.ps1";
        private const string PathFailureTest = @"D:\Work\Github\heartBeat\HeartBeat.Tests\Scripts\ShouldFail.ps1";
        private const string PathUnexpectedTest = @"D:\Work\Github\heartBeat\HeartBeat.Tests\Scripts\ShouldGiveUnexpectedResult.ps1";
        private const string PathExceptionTest = @"D:\Work\Github\heartBeat\HeartBeat.Tests\Scripts\ShouldCauseException.ps1";

        private const string Params = "Hello World";
        private const string WrongParams = "p1 p2";
        

        /*********************************************
        LOCAL TESTS
        *********************************************/
        [TestMethod]
        public void Check_Powershell_Script_Should_Return_True()
        {
            var actual = (new PowershellHealthCheck(TestName, PathSuccessTest, string.Empty)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Check_Powershell_Script_Should_Return_False()
        {
            var actual = (new PowershellHealthCheck(TestName, PathFailureTest, string.Empty)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Check_Powershell_Script_Unexpected_Result_Should_Return_False()
        {
            var actual = (new PowershellHealthCheck(TestName, PathUnexpectedTest, string.Empty)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(RuntimeException))]
        public void Check_Powershell_Script_Throw_Exception_Should_Raise_Exception()
        {
            (new PowershellHealthCheck(TestName, PathExceptionTest, string.Empty)).PerformCheck();
        }

        [TestMethod]
        public void Check_Powershell_Script_With_Parameters_Should_Return_True()
        {
            var actual = (new PowershellHealthCheck(TestName, PathSuccessTest, Params)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Check_Powershell_Script_With_Wrong_Parameters_Should_Return_False()
        {
            var actual = (new PowershellHealthCheck(TestName, PathSuccessTest, WrongParams)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Check_Powershell_Script_With_Parameters_Should_Return_False()
        {
            var actual = (new PowershellHealthCheck(TestName, PathFailureTest, Params)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Check_Powershell_Script_With_Parameters_Unexpected_Result_Should_Return_False()
        {
            var actual = (new PowershellHealthCheck(TestName, PathUnexpectedTest, Params)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(RuntimeException))]
        public void Check_Powershell_Script_With_Parameters_Throw_Exception_Should_Raise_Exception()
        {
            (new PowershellHealthCheck(TestName, PathExceptionTest, Params)).PerformCheck();
        }

    }
}
