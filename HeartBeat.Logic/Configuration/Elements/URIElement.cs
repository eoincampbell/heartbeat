using System.Configuration;
using System.Net;

namespace HeartBeat.Logic.Configuration.Elements
{
    public class URIElement : BaseElement
    {
        private const string URIProperty = "uri";
        [ConfigurationProperty(URIProperty, DefaultValue = "", IsKey = false, IsRequired = true)]
        public string URI
        {
            get { return (string)(base[URIProperty]); }
            set { base[URIProperty] = value; }
        }

        private const string HttpStatusCodeProperty = "status";
        [ConfigurationProperty(HttpStatusCodeProperty, DefaultValue=HttpStatusCode.OK, IsKey = false, IsRequired = false)]
        public HttpStatusCode Status
        {
            get { return (HttpStatusCode)(base[HttpStatusCodeProperty]); }
            set { base[HttpStatusCodeProperty] = value; }
        }

        private const string PatternMatchProperty = "match";
        [ConfigurationProperty(PatternMatchProperty, DefaultValue = "", IsKey = false, IsRequired = false)]
        public string PatternMatch
        {
            get { return (string)(base[PatternMatchProperty]); }
            set { base[PatternMatchProperty] = value; }
        }
    }
}
