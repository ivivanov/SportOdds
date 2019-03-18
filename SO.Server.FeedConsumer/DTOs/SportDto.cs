using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.DTOs
{
    public class SportDto
    {
        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement(ElementName = "Event")]
        public EventDto[] Events { get; set; }

    }
}
