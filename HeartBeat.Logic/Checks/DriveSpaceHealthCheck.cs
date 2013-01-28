using System;
using System.Linq;
using System.Management;
using HeartBeat.Logic.Helpers;

namespace HeartBeat.Logic.Checks
{
    /// <summary>
    /// HealthCheck which checks to make sure that a specific disk on a specific server
    /// has more than the specified threshold of 
    /// </summary>
    public class DriveSpaceHealthCheck : HealthCheck
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
                    "DISKSPACE : Free space on '{0}' now above threshold of {1:P}. It had been below threshold for {2}.";
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
            get { return "DISKSPACE : Free space on '{0}' is above threshold of {1:P}."; }
        }

        /// <summary>
        /// Gets the failure formatter.
        /// </summary>
        /// <value>
        /// The failure formatter.
        /// </value>
        protected override string FailureFormatter
        {
            get { return "DISKSPACE : Free space on '{0}' has fallen below threshold of {1:P}."; }
        }

        /// <summary>
        /// Gets to string formatter.
        /// </summary>
        /// <value>
        /// To string formatter.
        /// </value>
        protected override string ToStringFormatter
        {
            get { return "DISKSPACE : {0}"; }
        }

        /// <summary>
        /// This Health Checks Unique Id Formatter
        /// </summary>
        private const string IDENTIFIER = "drive_{0}_{1}";

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        private string ServerName{ get; set; }

        /// <summary>
        /// Gets or sets the drive letter.
        /// </summary>
        /// <value>
        /// The drive letter.
        /// </value>
        private string DriveLetter { get; set; }

        /// <summary>
        /// Gets or sets the threshold.
        /// </summary>
        /// <value>
        /// The threshold.
        /// </value>
        private double Threshold { get; set; }

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
        /// Gets or sets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        private string Domain { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DriveSpaceHealthCheck" /> class.
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="driveLetter">The drive letter.</param>
        /// <param name="threshold">The threshold.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public DriveSpaceHealthCheck(string friendlyName, string serverName, string driveLetter, double threshold, string domain, string username, string password)
            : base(string.Format(IDENTIFIER, serverName, driveLetter), friendlyName)
        {
            ServerName = serverName;
            DriveLetter = driveLetter;
            Threshold = threshold;
            Domain = domain;
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Performs the check.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the check succeeds otherwise, <c>false</c>.
        /// </returns>
        public override bool PerformCheck()
        {
            if(Threshold <= 0 || Threshold >= 1)
            {
                var msg = string.Format("Threshold for {0} must be a decimal value greater than 0 and less than 1",
                                           FriendlyName);
                throw new ArgumentException(msg);
            }

            var wmiServerString = string.Format("\\\\{0}\\root\\cimv2", ServerName);
            var diskQuery = string.Format("SELECT * FROM Win32_LogicalDisk WHERE Name = '{0}' AND DriveType = 3", DriveLetter);

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

            var qry = new ObjectQuery(diskQuery);
            var mos = new ManagementObjectSearcher(scope, qry);
            var result = mos.Get().Cast<ManagementObject>().FirstOrDefault();

            if (result == null)
                throw new ArgumentException("Invalid Drive Letter Provided");

            var totalSpace = Convert.ToDouble(result.GetPropertyValue("Size").ToString());
            var freeSpace = Convert.ToDouble(result.GetPropertyValue("FreeSpace").ToString());

            return (freeSpace/totalSpace) > Threshold;
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
            return string.Format(SuccessAfterFailureFormatter, FriendlyName, Threshold, ts.ToFriendlyString());
        }

        /// <summary>
        /// Gets the success message.
        /// </summary>
        /// <returns>
        /// The success message
        /// </returns>
        public override string GetSuccessMessage()
        {
            return string.Format(SuccessFormatter, FriendlyName, Threshold);
        }

        /// <summary>
        /// Gets the failure message for this health check on this server
        /// </summary>
        /// <returns>
        /// The failure message
        /// </returns>
        public override string GetFailureMessage()
        {
            return string.Format(FailureFormatter, FriendlyName, Threshold);
        }
    }
}
