using System.Configuration;
using HeartBeat.Logic.Configuration.Elements;

namespace HeartBeat.Logic.Configuration.Collections
{
    public sealed class ServiceCollection : BaseConfigurationElementCollection<ServiceElement>
    {
        private const string ElementTag = "service";

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
