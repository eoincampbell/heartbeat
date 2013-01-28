using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using HeartBeat.Logic.Configuration;
using HeartBeat.Logic.Helpers;

namespace HeartBeat.Logic.Checks
{
    /// <summary>
    /// Manager Class which orchestrates executing the different health checks
    /// </summary>
    public class HealthCheckManager
    {
        /// <summary>
        /// Dictionary, key'd by HealthCheck.Id which tracks when failures occur
        /// </summary>
        private readonly IDictionary<string, DateTime> _failureTracking;

        /// <summary>
        /// The name of this server
        /// </summary>
        private readonly string _thisServer;

        /// <summary>
        /// The address to which emails should be sent
        /// </summary>
        private readonly string _emailTo;

        /// <summary>
        /// The formatter for the mail notification subject
        /// </summary>
        private readonly string _emailSubjectFormat;

        /// <summary>
        /// Collection of all the healthchecks to be run.
        /// </summary>
        private readonly IList<IHealthCheck> _healthChecks;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCheckManager"/> class.
        /// </summary>
        public HealthCheckManager()
        {
            _failureTracking = new Dictionary<string, DateTime>();
            _thisServer = HeartBeatSettings.SettingsManager.ThisServer;
            _emailTo = HeartBeatSettings.SettingsManager.EmailTo;
            _emailSubjectFormat = HeartBeatSettings.SettingsManager.EmailSubjectFormat;
            _healthChecks = LoadHealthChecks();
        }

        /// <summary>
        /// Loads the health checks from the configuration sources
        /// </summary>
        /// <returns></returns>
        private static List<IHealthCheck> LoadHealthChecks()
        {
            var checks = new List<IHealthCheck>();

            HeartBeatSettings.SettingsManager.Servers.ToList().ForEach(
                s => checks.Add(new PingHealthCheck(s.FriendlyName, s.ServerName, s.PingTimeout)));

            HeartBeatSettings.SettingsManager.Services.ToList().ForEach(
                s => checks.Add(new ServiceHealthCheck(s.FriendlyName, s.ServerName, s.ServiceName, s.Domain, s.Username, s.Password)));

            HeartBeatSettings.SettingsManager.Queries.ToList().ForEach(
                s => checks.Add(new SqlQueryHealthCheck(s.FriendlyName, s.ConnectionStringKey, s.StoredProcedureName)));

            HeartBeatSettings.SettingsManager.URIs.ToList().ForEach(
                s => checks.Add(new URIHealthCheck(s.FriendlyName, s.URI, s.Status, s.PatternMatch)));

            HeartBeatSettings.SettingsManager.Drives.ToList().ForEach(
                s => checks.Add(new DriveSpaceHealthCheck(s.FriendlyName, s.ServerName, s.DriveLetter, s.Threshold, s.Domain, s.Username, s.Password)));

            HeartBeatSettings.SettingsManager.PowershellScripts.ToList().ForEach(
                s => checks.Add(new PowershellHealthCheck(s.FriendlyName, s.Path, s.Params)));

            HeartBeatSettings.SettingsManager.ScheduledTasks.ToList().ForEach(
                s => checks.Add(new TaskSchedulerHealthCheck(s.FriendlyName, s.TaskName, s.Server, s.Domain, s.UserName, s.Password)));

            
            return checks;
        }

        /// <summary>
        /// Performs the checks.
        /// </summary>
        public void PerformChecks()
        {
            var messages = new List<HealthCheckMessage>();
            var success = 0;
            foreach (var hc in _healthChecks)
            {
                Debug.WriteLine("{0}: {1}", hc, hc.FriendlyName);
                try
                {
                    var result = hc.PerformCheck();
                    if(result) success++;

                    var outputMessage = GetOutputMessage(result, hc);

                    if (outputMessage == null) continue;
                    if (outputMessage.SendMail)
                    {
                        messages.Add(outputMessage);
                    }
                            
                    LogHelper.Information(outputMessage.ToString());
                }
                catch(Exception ex)
                {
                    var outputMessage = GetOutputMessage(false, hc);

                    if (outputMessage == null) continue;
                    if (outputMessage.SendMail)
                    {
                        messages.Add(outputMessage);
                    }
                    LogHelper.Error(string.Format("EXCEPTION: {0}\n                      {1}", hc, outputMessage), ex);
                }
            }

            if(messages.Any())
            {
                SendMail(messages, _healthChecks.Count, success);
            }
        }

        /// <summary>
        /// Sends the email notifications
        /// </summary>
        /// <param name="messages">The health check messages to be sent.</param>
        /// <param name="totalChecks">The total number of checks performed</param>
        /// <param name="passedChecks">The number of checks which passed</param>
        private void SendMail(IEnumerable<HealthCheckMessage> messages, int totalChecks, int passedChecks)
        {
            if(string.IsNullOrEmpty(_emailTo)) return;

            try
            {

                string subject;
                try
                {
                    subject = string.Format(_emailSubjectFormat, _thisServer, DateTime.Now.Ticks/1000);
                }
                catch
                {
                    subject = string.Format("[Heartbeat] [{0}] (Default Subject) Notification Id:{1}", _thisServer, DateTime.Now.Ticks / 1000);
                }

                var body = Email.EmailBuilder.BuildMailBody(_thisServer, messages, totalChecks, passedChecks);

                var message = new MailMessage
                                  {
                                      Subject = subject,
                                      IsBodyHtml = true,
                                      BodyEncoding = Encoding.ASCII,
                                      Body = body
                                  };
                message.To.Add(_emailTo);

                var client = new SmtpClient();
                client.Send(message);
            }
            catch(Exception ex)
            {
                LogHelper.Error("Failed to send email. ", ex);    
            }
        }

        /// <summary>
        /// Gets the output message.
        /// </summary>
        /// <param name="result">if set to <c>true</c> health check was successful; otherwise <c>failed</c>.</param>
        /// <param name="hc">The hc.</param>
        /// <returns>The Formatted Message</returns>
        private HealthCheckMessage GetOutputMessage(bool result, IHealthCheck hc)
        {
            //Green Condition
            if (result && _failureTracking.ContainsKey(hc.Id))                 
            {
                var dt = _failureTracking[hc.Id];
                _failureTracking.Remove(hc.Id);
                return new HealthCheckMessage
                    {
                        Type = HealthCheckMessageType.CheckSucceeded,
                        Message = hc.GetSuccessMessage(dt),
                        SendMail = true
                    };
            }

            //Red Condition - New
            if (!result && !_failureTracking.ContainsKey(hc.Id))
            {
                _failureTracking.Add(hc.Id, DateTime.Now);
                return new HealthCheckMessage
                    {
                        Type = HealthCheckMessageType.CheckFailed,
                        Message = hc.GetFailureMessage(),
                        SendMail = true
                    };
            }

            //Red Condition - Repeated Issue
            if (!result && _failureTracking.ContainsKey(hc.Id))
            {
                var dt = _failureTracking[hc.Id];

                if (dt.AddMilliseconds(HeartBeatSettings.SettingsManager.RenotifyAfter) < DateTime.Now)
                {
                    //If the time it was logged at + the renotify after value < that now... alert again & bump the notification.
                    _failureTracking[hc.Id] = DateTime.Now;

                    return new HealthCheckMessage
                        {
                            Type = HealthCheckMessageType.CheckFailed,
                            Message = hc.GetFailureMessage(),
                            SendMail = true
                        };
                }

            }
            
            //Status Unchanged since last time - just log to file
            return new HealthCheckMessage
                {
                    Type = result ? HealthCheckMessageType.CheckSucceeded : HealthCheckMessageType.CheckFailed,
                    Message = result ? hc.GetSuccessMessage() : hc.GetFailureMessage(),
                    SendMail = false
                };
        }
    }
}
