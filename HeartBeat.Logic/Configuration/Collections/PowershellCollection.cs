using System.Configuration;
using HeartBeat.Logic.Configuration.Elements;

namespace HeartBeat.Logic.Configuration.Collections
{
    public sealed class PowershellCollection : BaseConfigurationElementCollection<PowershellElement>
    {
        private const string ElementTag = "powershellScript";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get { return ElementTag; }
        }
    }
}
