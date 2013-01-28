/*
 --TO RUN THESE Tests Execute the Following Sprocs on your database

USE [AirlineCrewpay]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE HeartbeatSuccessTest AS
BEGIN
	SET NOCOUNT ON;

    SELECT 1;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE HeartbeatFailureTest  AS
BEGIN
	SET NOCOUNT ON;

    SELECT 0;
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE HeartbeatInvalidTest  AS
BEGIN
	SET NOCOUNT ON;

    SELECT 'ASDF', '1234';
END
GO
 */

using System;
using HeartBeat.Logic.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeartBeat.Tests
{
    [TestClass]
    public class SqlQueryCheckTests
    {
        private const string ConnStrA = "QueryConnection";
        private const string ConnStrB = "QueryConnection_B";

        private const string SuccessSproc = "Sandbox.dbo.HeartbeatSuccessTest";
        private const string FailureSproc = "Sandbox.dbo.HeartbeatFailureTest";
        private const string InvalidSproc = "Sandbox.dbo.HeartbeatInvalidTest";

        [TestMethod]
        public void Query_Check_Call_Success_Sproc_Should_Return_True()
        {
            var actual = (new SqlQueryHealthCheck(SuccessSproc, ConnStrA, SuccessSproc)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Query_Check_Call_Success_Sproc_On_Alternative_Connection_String_Should_Return_True()
        {
            var actual = (new SqlQueryHealthCheck(SuccessSproc, ConnStrB, SuccessSproc)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Query_Check_Call_Failure_Sproc_Should_Return_False()
        {
            var actual = (new SqlQueryHealthCheck(FailureSproc, ConnStrA, FailureSproc)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Query_Check_Call_Invalid_Sproc_Should_Throw_FormatException()
        {
            (new SqlQueryHealthCheck(InvalidSproc, ConnStrA, InvalidSproc)).PerformCheck();
        }
    }
}
