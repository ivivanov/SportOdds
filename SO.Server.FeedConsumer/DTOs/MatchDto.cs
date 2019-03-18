using System;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.DTOs
{
    public class MatchDto
    {
        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public DateTime StartDate { get; set; }

        [XmlAttribute]
        public string MatchType { get; set; }

        [XmlElement(ElementName = "Bet")]
        public BetDto[] Bets { get; set; }
    }
}
