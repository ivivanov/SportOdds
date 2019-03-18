using SO.Server.FeedConsumer.Compares;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.DTOs
{
    public class EventDto : IEquatable<EventDto>
    {
        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public bool IsLive { get; set; }

        [XmlElement(ElementName = "Match")]
        public MatchDto[] Matches { get; set; }

        public bool Equals(EventDto other)
        {
            return Id.Equals(other.Id)
                 && Name.Equals(other.Name)
                 && (Matches.Except(other.Matches, new GenericComparer<MatchDto>()).Count() == 0);

        }

        public override int GetHashCode()
        {
            return Id.GetHashCode()
             ^ Name.GetHashCode()
             ^ Matches.Aggregate(0, (acc, x) => acc ^ x.GetHashCode());
        }
    }
}
