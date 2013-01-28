using System.Collections.Generic;
using System.Configuration;
using HeartBeat.Logic.Configuration.Collections;
using HeartBeat.Logic.Configuration.Elements;

namespace HeartBeat.Logic.Configuration
{
    public class HeartBeatSettings : ConfigurationSection
    {
        private const string SectionTag = "heartbeat/settings";

        private const string ServerCollectionProperty = "servers";
        private const string ServiceCollectionProperty = "services";
        private const string SqlQueryCollectionProperty = "queries";
        private const string DiskCollectionProperty = "drives";
        private const string URICollectionProperty = "uris";
        private const string PowershellCollectionProperty = "powershellScripts";
        private const string TaskSchedulerCollectionProperty = "scheduledTasks";
        
        private const string ThisServerProperty = "thisServer";
        private const string CheckIntervalProperty = "checkInterval";
        private const string EmailToProperty = "to";
        private const string EmailSubjectFormatProperty = "subjectFormat";
        private const string RenotifyAfterProperty = "renotifyAfter";
        
        private static HeartBeatSettings _settings
            = ConfigurationManager.GetSection(SectionTag) as HeartBeatSettings;

        public static HeartBeatSettings SettingsManager
        {
            get { return _settings ?? (_settings = ConfigurationManager.GetSection(SectionTag) as HeartBeatSettings); }
        }

        /// <summary>
        /// Gets the servers.
        /// </summary>
        [ConfigurationProperty(ServerCollectionProperty, IsDefaultCollection = true)]
        public ServerCollection Servers
        {
            get { return (ServerCollection)base[ServerCollectionProperty]; }
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        [ConfigurationProperty(ServiceCollectionProperty, IsDefaultCollection = false)]
        public ServiceCollection Services
        {
            get { return (ServiceCollection)base[ServiceCollectionProperty]; }
        }

        /// <summary>
        /// Gets the queries.
        /// </summary>
        [ConfigurationProperty(SqlQueryCollectionProperty, IsDefaultCollection = false)]
        public QueryCollection Queries
        {
            get { return (QueryCollection)base[SqlQueryCollectionProperty]; }
        }

        /// <summary>
        /// Gets the Drives.
        /// </summary>
        [ConfigurationProperty(DiskCollectionProperty, IsDefaultCollection = false)]
        public DriveCollection Drives
        {
            get { return (DriveCollection)base[DiskCollectionProperty]; }
        }

        /// <summary>
        /// Gets the URIs.
        /// </summary>
        [ConfigurationProperty(URICollectionProperty, IsDefaultCollection = false)]
        public URICollection URIs
        {
            get { return (URICollection) base[URICollectionProperty]; }
        }

        /// <summary>
        /// Gets the powershell scripts.
        /// </summary>
        /// <value>
        /// The powershell scripts.
        /// </value>
        [ConfigurationProperty(PowershellCollectionProperty, IsDefaultCollection = false)]
        public PowershellCollection PowershellScripts
        {
            get { return (PowershellCollection)base[PowershellCollectionProperty]; }
        }

        /// <summary>
        /// Gets the scheduled tasks.
        /// </summary>
        /// <value>
        /// The scheduled tasks.
        /// </value>
        [ConfigurationProperty(TaskSchedulerCollectionProperty, IsDefaultCollection = false)]
        public TaskSchedulerCollection ScheduledTasks
        {
            get { return (TaskSchedulerCollection)base[TaskSchedulerCollectionProperty]; }
        }

        

        /// <summary>
        /// Gets or sets this server.
        /// </summary>
        /// <value>
        /// The server property.
        /// </value>
        [ConfigurationProperty(ThisServerProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ThisServer
        {
            get { return (string)(base[ThisServerProperty]); }
            set { base[ThisServerProperty] = value; }
        }

        /// <summary>
        /// Gets or sets the check interval.
        /// </summary>
        /// <value>
        /// The check interval.
        /// </value>
        [ConfigurationProperty(CheckIntervalProperty, DefaultValue = 300000, IsKey = false, IsRequired = false)]
        public int CheckInterval
        {
            get { return (int)(base[CheckIntervalProperty]); }
            set { base[CheckIntervalProperty] = value; }
        }

        /// <summary>
        /// Gets or sets the email to.
        /// </summary>
        /// <value>
        /// The email to.
        /// </value>
        [ConfigurationProperty(EmailToProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string EmailTo
        {
            get { return (string)(base[EmailToProperty]); }
            set { base[EmailToProperty] = value; }
        }


        /// <summary>
        /// Gets or sets the email subject format.
        /// </summary>
        /// <value>
        /// The email subject format.
        /// </value>
        [ConfigurationProperty(EmailSubjectFormatProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string EmailSubjectFormat
        {
            get { return (string)(base[EmailSubjectFormatProperty]); }
            set { base[EmailSubjectFormatProperty] = value; }
        }


        /// <summary>
        /// Gets or sets the renotify after value, the time after which a previously alerted event should popup again.
        /// </summary>
        /// <value>
        /// The renotify after.
        /// </value>
        [ConfigurationProperty(RenotifyAfterProperty, DefaultValue = 3600000, IsKey = false, IsRequired = false)]
        public int RenotifyAfter
        {
            get { return (int)(base[RenotifyAfterProperty]); }
            set { base[RenotifyAfterProperty] = value; }
        }
        

    }
}
