using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;


namespace HeartBeat.Logic.Checks
{
    /// <summary>
    /// HealthCheck which attempts to get a 200 OK Response from a URL Web Request.
    /// Optional to request specific status OR look for a regex pattern in the response body.
    /// </summary>
    public class URIHealthCheck : HealthCheck
    {
        /// <summary>
        /// Gets the success after failure formatter.
        /// </summary>
        /// <value>
        /// The success after failure formatter.
        /// </value>
        protected override string SuccessAfterFailureFormatter
        {
            get { return "URI       : Request to '{0}' succeeded. URI was down for {1}."; }
        }

        /// <summary>
        /// Gets the success formatter.
        /// </summary>
        /// <value>
        /// The success formatter.
        /// </value>
        protected override string SuccessFormatter
        {
            get { return "URI       : Request to '{0}' succeeded."; }
        }

        /// <summary>
        /// Gets the failure formatter.
        /// </summary>
        /// <value>
        /// The failure formatter.
        /// </value>
        protected override string FailureFormatter
        {
            get { return "URI       : Request to '{0}' (HTTPSTATUS:{1}) failed "; }
        }

        /// <summary>
        /// Gets to string formatter.
        /// </summary>
        /// <value>
        /// To string formatter.
        /// </value>
        protected override string ToStringFormatter
        {
            get { return "URI       : {0}"; }
        }

        /// <summary>
        /// This Health Checks Unique Id Formatter
        /// </summary>
        private const string IDENTIFIER = "uri_{0}";

        /// <summary>
        /// Gets the URI.
        /// </summary>
        private string URI { get; set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        private HttpStatusCode Status { get; set; }

        /// <summary>
        /// Gets the pattern match.
        /// </summary>
        private string PatternMatch { get; set; }

        public URIHealthCheck(string friendlyName, string uri, HttpStatusCode status, string patternMatch)
            : base(string.Format(IDENTIFIER, friendlyName), friendlyName)
        {
            URI = uri;
            Status = status;
            PatternMatch = patternMatch;
        }

        /// <summary>
        /// Performs the check.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the check succeeds otherwise, <c>false</c>.
        /// </returns>
        public override bool PerformCheck()
        {
            HttpStatusCode hsc;
            var body = string.Empty;
            try
            {
                var req = (HttpWebRequest) WebRequest.Create(URI);
                var res = (HttpWebResponse) req.GetResponse();

                hsc = res.StatusCode;
                var resStream = res.GetResponseStream();
                if (resStream == null) return false;
                using (var reader = new StreamReader(resStream))
                {
                    body = reader.ReadToEnd();
                }
            }
            catch(WebException wex)
            {
                //4xx & 5xx statuses result in a web exception
                hsc = ((HttpWebResponse)wex.Response).StatusCode;
            }

            if (hsc != Status)
            {
                return false;
            }

            if(hsc == HttpStatusCode.OK && !string.IsNullOrEmpty(PatternMatch))
            {
                var match = Regex.Match(body, PatternMatch);

                if (!match.Success)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the failure message for this health check on this server
        /// </summary>
        /// <returns>
        /// The failure message
        /// </returns>
        public override string GetFailureMessage()
        {
            var response = string.Format(FailureFormatter, FriendlyName, Status);

            if (!String.IsNullOrEmpty(PatternMatch))
            {
                response = string.Format("{0} for regex '{1}'", response, PatternMatch);
            }

            return response;
        }   
    }
}
