using System;
using HeartBeat.Logic.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeartBeat.Tests
{
    [TestClass]
    public class TaskSchedulerTest
    {
        private const string TestName = "Test";

        private const string SuccessfulTaskName = "GoogleUpdateTaskMachineUA";
        private const string FailedTaskName = "Backup To Networzk"; //Misspelled
        private const string Server = "\\WIN2K3TEST";
        private const string Domain = "WIN2K3Test";
        private const string Username = "testuser";
        private const string Password = "password";

        [TestMethod]
        public void Check_TaskScheduler_Successful_Task_Should_Return_True()
        {
            var actual = (new TaskSchedulerHealthCheck(TestName, SuccessfulTaskName, Server, Domain, Username, Password)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Check_TaskScheduler_NonExistent_Task_Should_Return_Cause_Exception()
        {
            (new TaskSchedulerHealthCheck(TestName, FailedTaskName, Server, Domain, Username, Password)).PerformCheck();

        }
    }
}
