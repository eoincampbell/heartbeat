using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HeartBeat.Logic.Checks
{
    /// <summary>
    /// HealthCheck which executes Stored Procedures and raises a success/failure
    /// dependent on whether the scalar result of the procedure is 1 or 0
    /// </summary>
    public class SqlQueryHealthCheck : HealthCheck
    {
        /// <summary>
        /// Gets the success after failure formatter.
        /// </summary>
        /// <value>
        /// The success after failure formatter.
        /// </value>
        protected override string SuccessAfterFailureFormatter
        {
            get { return "SPROC     : Procedure '{0}' succeeded. It had been failing for {1}."; }
        }

        /// <summary>
        /// Gets the success formatter.
        /// </summary>
        /// <value>
        /// The success formatter.
        /// </value>
        protected override string SuccessFormatter
        {
            get { return "SPROC     : Procedure '{0}' succeeded."; }
        }

        /// <summary>
        /// Gets the failure formatter.
        /// </summary>
        /// <value>
        /// The failure formatter.
        /// </value>
        protected override string FailureFormatter
        {
            get { return "SPROC     : Procedure '{0}' failed."; }
        }

        /// <summary>
        /// Gets to string formatter.
        /// </summary>
        /// <value>
        /// To string formatter.
        /// </value>
        protected override string ToStringFormatter
        {
            get { return "SPROC     : {0}"; }
        }

        /// <summary>
        /// This Health Checks Unique Id Formatter
        /// </summary>
        private const string IDENTIFIER = "query_{0}_{1}";

        /// <summary>
        /// Gets the ConnectionString key.
        /// </summary>
        private string ConnectionStringKey { get; set; }
        
        /// <summary>
        /// Gets the stored proc.
        /// </summary>
        private string StoredProc { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlQueryHealthCheck"/> class.
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="connStrKey">the connection string key.</param>
        /// <param name="storedProc">The stored proc.</param>
        public SqlQueryHealthCheck(string friendlyName, string connStrKey, string storedProc)
            : base(string.Format(IDENTIFIER, connStrKey, storedProc.Replace('.', '_')), friendlyName)
        {
            ConnectionStringKey = connStrKey;
            StoredProc = storedProc;
        }

        /// <summary>
        /// Performs the check.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the check succeeds otherwise, <c>false</c>.
        /// </returns>
        public override bool PerformCheck()
        {
            var connstr = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            bool result;
            using (var conn = new SqlConnection(connstr))
            {
                conn.Open();
                using(var cmd = new SqlCommand(StoredProc, conn) { CommandType = CommandType.StoredProcedure })
                {
                    var o = cmd.ExecuteScalar();
                    result = Convert.ToBoolean(o);
                }
            }
            return result;
        }
    }
}
