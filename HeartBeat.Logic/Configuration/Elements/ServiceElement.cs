using System.Configuration;

namespace HeartBeat.Logic.Configuration.Elements
{
    public class ServiceElement : BaseElement
    {

        private const string ServerNameProperty = "serverName";
        [ConfigurationProperty(ServerNameProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ServerName
        {
            get { return (string)(base[ServerNameProperty]); }
            set { base[ServerNameProperty] = value; }
        }

        private const string ServiceNameProperty = "serviceName";
        [ConfigurationProperty(ServiceNameProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ServiceName
        {
            get { return (string)(base[ServiceNameProperty]); }
            set { base[ServiceNameProperty] = value; }
        }

        private const string DomainProperty = "domain";
        [ConfigurationProperty(DomainProperty, DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Domain
        {
            get { return (string)(base[DomainProperty]); }
            set { base[DomainProperty] = value; }
        }

        private const string UsernameProperty = "username";
        [ConfigurationProperty(UsernameProperty, DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Username
        {
            get { return (string)(base[UsernameProperty]); }
            set { base[UsernameProperty] = value; }
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
