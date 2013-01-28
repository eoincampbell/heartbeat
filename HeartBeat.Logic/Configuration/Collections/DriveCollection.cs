using System.Configuration;
using HeartBeat.Logic.Configuration.Elements;

namespace HeartBeat.Logic.Configuration.Collections
{
    public sealed class DriveCollection : BaseConfigurationElementCollection<DriveElement>
    {
        private const string ElementTag = "drive";

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
