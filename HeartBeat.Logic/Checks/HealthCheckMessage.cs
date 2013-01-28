namespace HeartBeat.Logic.Checks
{
    /// <summary>
    /// Enumeration Representing whether a message is associated with a successful or failed check
    /// </summary>
    public enum HealthCheckMessageType
    {
        CheckFailed = 1,
        CheckSucceeded = 2
    }

    /// <summary>
    /// Wrapper Object representing a formatted output message from a HealthCheck
    /// </summary>
    public class HealthCheckMessage
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public HealthCheckMessageType Type { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [send mail].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [send mail]; otherwise, <c>false</c>.
        /// </value>
        public bool SendMail { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}: {1}",
                                 ((Type == HealthCheckMessageType.CheckSucceeded) ? "SUCCESS  " : "FAILURE  "),
                                 Message);
        }
    }
}
