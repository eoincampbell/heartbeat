/***************************************************
 * 
 * If you're considering Changing this, keep in mind the large majority
 * of mail clients don't honour non-inline css.. hence the big fugly html resources
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using HeartBeat.Logic.Checks;

namespace HeartBeat.Logic.Email
{
    public static class EmailBuilder
    {
        private const string CssSuccess = "background-color: #99FF99; color: #000000;";
        private const string CssFailure = "background-color: #FF6666; color: #FFFFFF; font-weight:bold;";

        public static string BuildMailBody(string server, IEnumerable<HealthCheckMessage> messages, int totalChecks, int passedChecks)
        {
            var  body = new StringBuilder();
            body.Append(Resources.EmailResources.MailStart);
            body.AppendFormat(Resources.EmailResources.SummaryFormatter, server, DateTime.Now, totalChecks, passedChecks);
            body.Append(Resources.EmailResources.StatusesStart);

            foreach(var m in messages)
            {
                var color = (m.Type == HealthCheckMessageType.CheckSucceeded) ? CssSuccess : CssFailure;

                body.AppendFormat(Resources.EmailResources.StatusFormatter, color, m.Message);
            }

            body.Append(Resources.EmailResources.StatusesEnd);
            body.Append(Resources.EmailResources.MailEnd);
            return body.ToString();
        }

    }
}
