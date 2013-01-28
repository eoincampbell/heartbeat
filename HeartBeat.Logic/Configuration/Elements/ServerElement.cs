using System.Configuration;

namespace HeartBeat.Logic.Configuration.Elements
{
    public class ServerElement :BaseElement
    {
        private const string ServerNameProperty = "serverName";
        [ConfigurationProperty(ServerNameProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ServerName
        {
            get { return (string)(base[ServerNameProperty]); }
            set { base[ServerNameProperty] = value; }
        }

        private const string PingTimeoutProperty = "pingTimeout";
        [ConfigurationProperty(PingTimeoutProperty, DefaultValue = 2000, IsKey = false, IsRequired = false)]
        public int PingTimeout
        {
            get { return (int)(base[PingTimeoutProperty]); }
            set { base[PingTimeoutProperty] = value; }
        }
    }
}
