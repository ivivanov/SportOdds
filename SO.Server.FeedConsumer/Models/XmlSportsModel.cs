using SO.Server.FeedConsumer.Utils;
using System;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.Models
{
    [XmlRoot(ElementName = "XmlSports")]
    public class XmlSportsModel
    {
        [XmlAttribute]
        public DateTime CreateDate { get; set; }

        [XmlElement]
        public SportModel Sport { get; set; }

        public static XmlSportsModel Create(string xml)
        {
            return XmlUtils.Deserialize<XmlSportsModel>(xml);
        }
    }
}
