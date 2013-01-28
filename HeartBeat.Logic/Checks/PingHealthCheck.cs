using HeartBeat.Logic.Helpers;

namespace HeartBeat.Logic.Checks
{
    /// <summary>
    /// Health Check which pings another server to see if that server is "Up"
    /// </summary>
    public class PingHealthCheck : HealthCheck
    {
        /// <summary>
        /// Gets the success after failure formatter.
        /// </summary>
        /// <value>
        /// The success after failure formatter.
        /// </value>
        protected override string SuccessAfterFailureFormatter
        {
            get
            {
                return
                    "PING      : Successfully pinged '{0}'. Server was out of contact {1}.";
            }
        }

        /// <summary>
        /// Gets the success formatter.
        /// </summary>
        /// <value>
        /// The success formatter.
        /// </value>
        protected override string SuccessFormatter
        {
            get { return "PING      : Successfully pinged '{0}'."; }
        }

        /// <summary>
        /// Gets the failure formatter.
        /// </summary>
        /// <value>
        /// The failure formatter.
        /// </value>
        protected override string FailureFormatter
        {
            get { return "PING      : Failed to ping '{0}'."; }
        }

        /// <summary>
        /// Gets to string formatter.
        /// </summary>
        /// <value>
        /// To string formatter.
        /// </value>
        protected override string ToStringFormatter
        {
            get { return "PING      : {0}"; }
        }

        /// <summary>
        /// This Health Checks Unique Id Formatter
        /// </summary>
        private const string IDENTIFIER = "server_{0}";
        /// <summary>
        /// Gets the name of the server.
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        private string ServerName { get; set; }

        /// <summary>
        /// Gets the ping timeout in milliseconds
        /// </summary>
        private int PingTimeout { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PingHealthCheck"/> class.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="pingTimeout">The length of time before the ping should timeout in milliseconds</param>
        public PingHealthCheck(string friendlyName, string serverName, int pingTimeout)
            : base(string.Format(IDENTIFIER, serverName), friendlyName)
        {
            ServerName = serverName;
            PingTimeout = pingTimeout;
        }

        /// <summary>
        /// Performs the check.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the check succeeds otherwise, <c>false</c>.
        /// </returns>
        public override bool PerformCheck()
        {
            return Pinger.PingServer(ServerName, PingTimeout);
        }
    }
}
