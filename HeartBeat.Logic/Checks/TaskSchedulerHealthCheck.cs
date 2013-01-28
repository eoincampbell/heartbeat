using TS = Microsoft.Win32.TaskScheduler;

namespace HeartBeat.Logic.Checks
{
    public class TaskSchedulerHealthCheck : HealthCheck
    {
        /// <summary>
        /// Gets the success after failure formatter.
        /// </summary>
        /// <value>
        /// The success after failure formatter.
        /// </value>
        protected override string SuccessAfterFailureFormatter
        {
            get { return "SCHTASK   : Task '{0}' succeeded. It had been failing for {1}."; }
        }

        /// <summary>
        /// Gets the success formatter.
        /// </summary>
        /// <value>
        /// The success formatter.
        /// </value>
        protected override string SuccessFormatter
        {
            get { return "SCHTASK   : Task '{0}' succeeded."; }
        }

        /// <summary>
        /// Gets the failure formatter.
        /// </summary>
        /// <value>
        /// The failure formatter.
        /// </value>
        protected override string FailureFormatter
        {
            get { return "SCHTASK   : Task '{0}' failed."; }
        }

        /// <summary>
        /// Gets to string formatter.
        /// </summary>
        /// <value>
        /// To string formatter.
        /// </value>
        protected override string ToStringFormatter
        {
            get { return "SCHTASK   : {0}"; }
        }

        /// <summary>
        /// The IDENTIFIER
        /// </summary>
        private const string IDENTIFIER = "scheduledtask_{0}";

        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        /// <value>
        /// The name of the task.
        /// </value>
        private string TaskName { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        private string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        private string Domain { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        private string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        private string Password { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskSchedulerHealthCheck" /> class.
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="taskName">Name of the task.</param>
        /// <param name="serverName">The server.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="username">The user.</param>
        /// <param name="password">The password.</param>
        public TaskSchedulerHealthCheck(string friendlyName, string taskName, string serverName, string domain, string username, string password)
            : base(string.Format(IDENTIFIER, friendlyName),friendlyName)
        {
            TaskName = taskName;
            ServerName = serverName;
            Domain = domain;
            UserName = username;
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
            using (var ts = new TS.TaskService(ServerName, UserName, Domain, Password))
            {
                var task = ts.GetTask(TaskName);

                return task.LastTaskResult == 0;
            }
        }
    }
}
