using System;

namespace HeartBeat.Logic.Checks
{
    /// <summary>
    /// This interface represents the contract for a single non-specific check.
    /// A HealthCheck must be capable of Identifying itself uniquely, performing a success/fail based check
    /// and returning senibly formatted success / failure messages to the caller in teh context of the check that has run.
    /// </summary>
    public interface IHealthCheck
    {
        /// <summary>
        /// Performs the check.
        /// </summary>
        /// <returns><c>true</c> if the check succeeds otherwise, <c>false</c>.</returns>
        bool PerformCheck();

        /// <summary>
        /// The unique identifier for this health check
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The unique identifier for this health check
        /// </summary>
        string FriendlyName { get; }

        /// <summary>
        /// Gets the success message for this health check on this server
        /// </summary>
        /// <param name="downSince">the datetime that this health check starting failing.</param>
        /// <returns>The success message</returns>
        string GetSuccessMessage(DateTime downSince);

        /// <summary>
        /// Gets the success message.
        /// </summary>
        /// <returns>The success message</returns>
        string GetSuccessMessage();

        /// <summary>
        /// Gets the failure message for this health check on this server
        /// </summary>
        /// <returns>The failure message</returns>
        string GetFailureMessage();
    }
}
