using System;

namespace HeartBeat.Logic.Helpers
{
    /// <summary>
    /// Extensions Class
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        /// Takes a time span and converts it to a user friendly, human readable string
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        /// <returns>The user friendly string</returns>
        public static string ToFriendlyString(this TimeSpan elapsed)
        {
            var accountedDays = elapsed.Days;

            var years = accountedDays / 365;
            var hasYears = years > 0;
            accountedDays -= years * 365;

            var months = accountedDays / 30;
            var hasMonths = months > 0;
            accountedDays -= months * 30;

            var weeks = accountedDays / 7;
            var hasWeeks = weeks > 0;
            accountedDays -= weeks * 7;

            var days = accountedDays;
            var hasDays = days > 0;

            var hasHours = elapsed.Hours > 0 && elapsed.Hours < 24;
            var hasMinutes = elapsed.Minutes > 0 && elapsed.Minutes < 60;

            if (hasYears)
            {
                if (years > 1)
                {
                    return string.Format("for {0} years", years);
                }

                if (hasMonths)
                {
                    return string.Format("for {0} year {1} month{2}",
                                         years,
                                         months,
                                         months > 1 ? "s" : string.Empty);
                }
            }

            if (hasMonths)
            {
                return months == 1 ? "since last month" : string.Format("for {0} months", months);
            }

            if (hasWeeks)
            {
                return weeks == 1 ? "since last week" : string.Format("for {0} weeks", weeks);
            }

            if (hasDays)
            {
                return days == 1 ? "since yesterday" : string.Format("for {0} days", days);
            }

            if (hasHours)
            {
                return string.Format("for {0} hour{1}",
                    elapsed.Hours,
                    elapsed.Hours > 1 ? "s" : string.Empty);
            }

            if (hasMinutes)
            {
                return string.Format("for {0} minute{1}",
                    elapsed.Minutes,
                    elapsed.Minutes > 1 ? "s" : string.Empty);
            }

            return string.Format("for {0} second{1}",
                elapsed.Seconds,
                elapsed.Seconds > 1 ? "s" : string.Empty);
        }
    }
}
