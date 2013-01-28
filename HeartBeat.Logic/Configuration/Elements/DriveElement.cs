using System.Configuration;

namespace HeartBeat.Logic.Configuration.Elements
{
    public class DriveElement : BaseElement
    {
        private const string ServerNameProperty = "serverName";
        [ConfigurationProperty(ServerNameProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ServerName
        {
            get { return (string)(base[ServerNameProperty]); }
            set { base[ServerNameProperty] = value; }
        }

        private const string DriveLetterProperty = "driveLetter";
        [ConfigurationProperty(DriveLetterProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string DriveLetter
        {
            get { return (string)(base[DriveLetterProperty]); }
            set { base[DriveLetterProperty] = value; }
        }

        private const string ThresholdProperty = "threshold";
        [ConfigurationProperty(ThresholdProperty, DefaultValue=0.1, IsKey = false, IsRequired = true)]
        public double Threshold
        {
            get { return (double)(base[ThresholdProperty]); }
            set { base[ThresholdProperty] = value; }
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
