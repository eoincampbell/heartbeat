using System.Configuration;

namespace HeartBeat.Logic.Configuration.Elements
{
    public class QueryElement : BaseElement
    {
        private const string ConnectionStringKeyProperty = "connectionStringKey";
        [ConfigurationProperty(ConnectionStringKeyProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ConnectionStringKey
        {
            get { return (string)(base[ConnectionStringKeyProperty]); }
            set { base[ConnectionStringKeyProperty] = value; }
        }

        private const string StoredProcedureNameProperty = "storedProc";
        [ConfigurationProperty(StoredProcedureNameProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string StoredProcedureName
        {
            get { return (string)(base[StoredProcedureNameProperty]); }
            set { base[StoredProcedureNameProperty] = value; }
        }
    }
}
