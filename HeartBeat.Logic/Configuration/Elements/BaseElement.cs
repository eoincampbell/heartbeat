using System.Configuration;

namespace HeartBeat.Logic.Configuration.Elements
{
    public class BaseElement : ConfigurationElement
    {
        private const string FriendlyNameProperty = "friendlyName";
        [ConfigurationProperty(FriendlyNameProperty, DefaultValue = "", IsKey = true, IsRequired = false)]
        public string FriendlyName
        {
            get { return (string)(base[FriendlyNameProperty]); }
            set { base[FriendlyNameProperty] = value; }
        }

        public override string ToString()
        {
            return FriendlyName;
        }
    }
}
