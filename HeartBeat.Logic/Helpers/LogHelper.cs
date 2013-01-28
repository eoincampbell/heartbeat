using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HeartBeat.Logic.Helpers
{
    /// <summary>
    /// Enterprise Logging Helper... Mostly taken from RBT.Utilities so as not to have a Dependency on that.
    /// </summary>
    public static class LogHelper
    {
        private static readonly object ClientLock = new object();
        private static LogWriter _writer;
        private const string GeneralCategory = "General";
        private const string Padding = "                      ";

        private static LogWriter Writer
        {
            get
            {
                lock (ClientLock)
                {
                    return _writer ?? (_writer = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>());
                }
            }
        }

        static LogHelper()
        {
        }

        public static void Verbose(string message, Exception exception = null, string title = "", int eventId = 1,
                                   Dictionary<string, object> extendedProperties = null)
        {
            TryWrite(message, exception, title, eventId, extendedProperties, LoggingPriority.Verbose,
                     TraceEventType.Verbose);
        }

        public static void Information(string message, Exception exception = null, string title = "", int eventId = 1,
                                       Dictionary<string, object> extendedProperties = null)
        {
            TryWrite(message, exception, title, eventId, extendedProperties, LoggingPriority.Information,
                     TraceEventType.Information);
        }

        public static void Warning(string message, Exception exception = null, string title = "", int eventId = 1,
                                   Dictionary<string, object> extendedProperties = null)
        {
            TryWrite(message, exception, title, eventId, extendedProperties, LoggingPriority.Warning,
                     TraceEventType.Warning);
        }

        public static void Error(string message, Exception exception = null, string title = "", int eventId = 1,
                                 Dictionary<string, object> extendedProperties = null)
        {
            TryWrite(message, exception, title, eventId, extendedProperties, LoggingPriority.Error, TraceEventType.Error);
        }

        public static void Critical(string message, Exception exception = null, string title = "", int eventId = 1,
                                    Dictionary<string, object> extendedProperties = null)
        {
            TryWrite(message, exception, title, eventId, extendedProperties, LoggingPriority.Critical,
                     TraceEventType.Critical);
        }

        private static void TryWrite(string message, Exception exception, string title, int eventId,
                                     IDictionary<string, object> extendedProperties, LoggingPriority priorty,
                                     TraceEventType traceEventType)
        {
            if (!Writer.IsLoggingEnabled()) return;

            Writer.Write(FormatMessage(message, exception), GeneralCategory, (int) priorty, eventId, traceEventType,
                         title, extendedProperties);
        }

        private static string FormatMessage(string message, Exception exception)
        {
            var ifp = (IFormatProvider) CultureInfo.InvariantCulture;

            if(exception == null) 
                return string.Format(ifp, "{0}", message);
           
            var strExcetption = exception.ToString().Split('\n');
            var fmtException = string.Join(Padding, strExcetption);
            return string.Format(ifp, "{0}{1}{2}{3}", message, Environment.NewLine, Padding, fmtException);
        }

        private enum LoggingPriority
        {
            Critical = 10,
            Error = 20,
            Warning = 30,
            Information = 40,
            Verbose = 50,
        }
    }
}
