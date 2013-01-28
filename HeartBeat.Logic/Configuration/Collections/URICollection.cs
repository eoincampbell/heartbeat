using System.Configuration;
using HeartBeat.Logic.Configuration.Elements;

namespace HeartBeat.Logic.Configuration.Collections
{
    public sealed class URICollection : BaseConfigurationElementCollection<URIElement>
    {
        private const string ElementTag = "uri";

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
