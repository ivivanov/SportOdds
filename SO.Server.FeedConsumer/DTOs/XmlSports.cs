using System;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.DTOs
{
    [XmlRoot(ElementName = "XmlSports")]
    public class XmlSports
    {
        public DateTime CreateDate { get; set; }

        [XmlElement]
        public SportDto Sport { get; set; }
    }
}
