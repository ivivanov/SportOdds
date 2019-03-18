using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.DTOs
{
    public class EventDto
    {
        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public bool IsLive { get; set; }

        [XmlElement(ElementName = "Match")]
        public MatchDto[] Matches { get; set; }
    }
}
