using System.Configuration;

namespace HeartBeat.Logic.Configuration.Elements
{
    public class PowershellElement : BaseElement
    {
        private const string PathProperty = "path";
        [ConfigurationProperty(PathProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Path
        {
            get { return (string)(base[PathProperty]); }
            set { base[PathProperty] = value; }
        }

        private const string ParamsProperty = "params";
        [ConfigurationProperty(ParamsProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Params
        {
            get { return (string)(base[ParamsProperty]); }
            set { base[ParamsProperty] = value; }
        }
    }
}
