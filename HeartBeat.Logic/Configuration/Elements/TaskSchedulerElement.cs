using System.Configuration;

namespace HeartBeat.Logic.Configuration.Elements
{
    public class TaskSchedulerElement : BaseElement
    {
        private const string TaskNameProperty = "taskName";
        [ConfigurationProperty(TaskNameProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string TaskName
        {
            get { return (string)(base[TaskNameProperty]); }
            set { base[TaskNameProperty] = value; }
        }

        private const string ServerProperty = "serverName";
        [ConfigurationProperty(ServerProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Server
        {
            get { return (string)(base[ServerProperty]); }
            set { base[ServerProperty] = value; }
        }

        private const string DomainProperty = "domain";
        [ConfigurationProperty(DomainProperty, DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Domain
        {
            get { return (string)(base[DomainProperty]); }
            set { base[DomainProperty] = value; }
        }

        private const string UserNameProperty = "username";
        [ConfigurationProperty(UserNameProperty, DefaultValue = "", IsKey = false, IsRequired = false)]
        public string UserName
        {
            get { return (string)(base[UserNameProperty]); }
            set { base[UserNameProperty] = value; }
        }

        private const string PasswordProperty = "password";
        [ConfigurationProperty(PasswordProperty, DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Password
        {
            get { return (string)(base[PasswordProperty]); }
            set { base[PasswordProperty] = value; }
        }
    }
}
