using System;
using System.Linq;
using System.Management;
using HeartBeat.Logic.Helpers;

namespace HeartBeat.Logic.Checks
{
    /// <summary>
    /// Health Check which queries the services running on the server and checks if this specific service is running.
    /// </summary>
    public class ServiceHealthCheck : HealthCheck
    {
        /// <summary>
        /// Gets the success after failure formatter.
        /// </summary>
        /// <value>
        /// The success after failure formatter.
        /// </value>
        protected override string SuccessAfterFailureFormatter
        {
            get { return "SERVICE   : {0} ({1} on {2}) is now running. Service was down for {3}."; }
        }

        /// <summary>
        /// Gets the success formatter.
        /// </summary>
        /// <value>
        /// The success formatter.
        /// </value>
        protected override string SuccessFormatter
        {
            get { return "SERVICE   : {0} ({1} on {2}) is running."; }
        }

        /// <summary>
        /// Gets the failure formatter.
        /// </summary>
        /// <value>
        /// The failure formatter.
        /// </value>
        protected override string FailureFormatter
        {
            get { return "SERVICE   : {0} ({1} on {2}) is down."; }
        }

        /// <summary>
        /// Gets to string formatter.
        /// </summary>
        /// <value>
        /// To string formatter.
        /// </value>
        protected override string ToStringFormatter
        {
            get { return "SERVICE   : {0}"; }
        }

        /// <summary>
        /// This Health Checks Unique Id Formatter
        /// </summary>
        private const string IDENTIFIER = "service_{0}_{1}";
        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        private string ServerName { get; set; }
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        private string ServiceName { get; set; }
        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        private string Domain { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        private string Username { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        private string Password { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHealthCheck"/> class.
        /// </summary>
        /// <param name="friendlyName">The User Friendly Name of the Server.</param>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public ServiceHealthCheck(string friendlyName, string serverName, string serviceName, string domain, string username, string password)
            : base(string.Format(IDENTIFIER, serverName, serviceName), friendlyName)
        {
            ServerName = serverName;
            ServiceName = serviceName;
            Username = username;
            Password = password;
            Domain = domain;
        }

        /// <summary>
        /// Performs the check.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the check succeeds otherwise, <c>false</c>.
        /// </returns>
        public override bool PerformCheck()
        {
            var wmiServerString = string.Format("\\\\{0}\\root\\cimv2", ServerName);
            var serviceQuery = string.Format("Select * from Win32_Service where Name = '{0}'", ServiceName);

            var op = new ConnectionOptions();

            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                var username = string.IsNullOrEmpty(Domain)
                                      ? Username
                                      : string.Format("{0}\\{1}", Domain, Username);

                op = new ConnectionOptions { Username = username, Password = Password };
            }
                
            var scope = new ManagementScope(wmiServerString, op);
            scope.Connect();

            var qry = new ObjectQuery(serviceQuery);
            var mos = new ManagementObjectSearcher(scope, qry);
            var result = mos.Get().Cast<ManagementObject>().FirstOrDefault();

            if (result == null)
                throw new ArgumentException("Invalid Service Name Provided");

            return result.GetPropertyValue("State").ToString().ToLower().Equals("running");

        }

        /// <summary>
        /// Gets the success message for this health check on this server
        /// </summary>
        /// <param name="downSince">the datetime that this health check starting failing.</param>
        /// <returns>
        /// The success message
        /// </returns>
        public override string GetSuccessMessage(DateTime downSince)
        {
            var ts = DateTime.Now - downSince;
            return string.Format(SuccessAfterFailureFormatter, FriendlyName, ServiceName, ServerName, ts.ToFriendlyString());
        }

        /// <summary>
        /// Gets the success message.
        /// </summary>
        /// <returns>
        /// The success message
        /// </returns>
        public override string GetSuccessMessage()
        {
            return string.Format(SuccessFormatter, FriendlyName, ServiceName, ServerName);
        }

        /// <summary>
        /// Gets the failure message for this health check on this server
        /// </summary>
        /// <returns>
        /// The failure message
        /// </returns>
        public override string GetFailureMessage()
        {
            return string.Format(FailureFormatter, FriendlyName ,ServiceName, ServerName);
        }


    }
}
