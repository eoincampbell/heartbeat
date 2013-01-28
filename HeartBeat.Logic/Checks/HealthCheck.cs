using System;
using HeartBeat.Logic.Helpers;

namespace HeartBeat.Logic.Checks
{
    public abstract class HealthCheck : IHealthCheck
    {
        protected abstract string SuccessAfterFailureFormatter { get; }
        protected abstract string SuccessFormatter { get; }
        protected abstract string FailureFormatter { get; }
        protected abstract string ToStringFormatter { get; }

        /// <summary>
        /// The unique identifier for this health check
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        public string FriendlyName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCheck"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="friendlyName">The user friendly name of this health check</param>
        protected HealthCheck(string id, string friendlyName)
        {
            Id = id;
            FriendlyName = friendlyName;
        }

        /// <summary>
        /// Performs the check.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the check succeeds otherwise, <c>false</c>.
        /// </returns>
        public abstract bool PerformCheck();
        
        /// <summary>
        /// Gets the success message for this health check on this server
        /// </summary>
        /// <param name="downSince">the datetime that this health check starting failing.</param>
        /// <returns>
        /// The success message
        /// </returns>
        public virtual string GetSuccessMessage(DateTime downSince)
        {
            var ts = DateTime.Now - downSince;
            return string.Format(SuccessAfterFailureFormatter, FriendlyName, ts.ToFriendlyString());
        }

        /// <summary>
        /// Gets the success message.
        /// </summary>
        /// <returns>
        /// The success message
        /// </returns>
        public virtual string GetSuccessMessage()
        {
            return string.Format(SuccessFormatter, FriendlyName);
        }

        /// <summary>
        /// Gets the failure message for this health check on this server
        /// </summary>
        /// <returns>
        /// The failure message
        /// </returns>
        public virtual string GetFailureMessage()
        {
            return string.Format(FailureFormatter, FriendlyName);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(ToStringFormatter, FriendlyName);
        }
    }
}
