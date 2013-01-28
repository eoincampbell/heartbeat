using System.Net;
using HeartBeat.Logic.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeartBeat.Tests
{
    [TestClass]
    public class URICheckTests
    {
        private const string URI200 = "http://localhost/page200.html";
        private const string URI404 = "http://localhost/page404.aspx";
        private const string URI500 = "http://localhost/page500.asp";
        private const string URIName = "MyTest";

        private const string CorrectPattern = "test";
        private const string IncorrectPattern = "donut";

        

        [TestMethod]
        public void URI_Check_Request_URI_Expecting_200_OK_Status_Should_Return_True()
        {
            var actual = (new URIHealthCheck(URIName, URI200, HttpStatusCode.OK, string.Empty )).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void URI_Check_Request_Non_Existent_URI_Expecting_200_OK_Status_Should_Return_False()
        {
            var actual = (new URIHealthCheck(URIName, URI404, HttpStatusCode.OK, string.Empty)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void URI_Check_Request_ServerException_URI_Expecting_200_OK_Status_Should_Return_False()
        {
            var actual = (new URIHealthCheck(URIName, URI500, HttpStatusCode.OK, string.Empty)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void URI_Check_Request_URI_Expecting_200_OK_Status_And_Specific_Pattern_Should_Return_True()
        {
            var actual = (new URIHealthCheck(URIName, URI200, HttpStatusCode.OK, CorrectPattern)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void URI_Check_Request_URI_Expecting_200_OK_Status_And_Specific_Pattern_Should_Return_False()
        {
            var actual = (new URIHealthCheck(URIName, URI200, HttpStatusCode.OK, IncorrectPattern)).PerformCheck();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void URI_Check_Request_URI_Expecting_404_FileNotFound_Status_Should_Return_True()
        {
            var actual = (new URIHealthCheck(URIName, URI404, HttpStatusCode.NotFound, string.Empty)).PerformCheck();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void URI_Check_Request_URI_Expecting_500_FileNotFound_Status_Should_Return_True()
        {
            var actual = (new URIHealthCheck(URIName, URI500, HttpStatusCode.InternalServerError, string.Empty)).PerformCheck();

            Assert.IsTrue(actual);
        }


    }
}
