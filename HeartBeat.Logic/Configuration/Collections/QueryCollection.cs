using System.Configuration;
using HeartBeat.Logic.Configuration.Elements;

namespace HeartBeat.Logic.Configuration.Collections
{
    public sealed class QueryCollection : BaseConfigurationElementCollection<QueryElement>
    {
        private const string ElementTag = "query";

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
