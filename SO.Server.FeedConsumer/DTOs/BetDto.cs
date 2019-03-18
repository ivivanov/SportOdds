using SO.Server.FeedConsumer.Comparers;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.DTOs
{
    public class BetDto : IEquatable<BetDto>
    {
        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public bool IsLive { get; set; }

        [XmlElement(ElementName = "Odd")]
        public OddDto[] Odds { get; set; }

        public bool Equals(BetDto other)
        {
            return Id.Equals(other.Id)
                 && Name.Equals(other.Name)
                 && IsLive.Equals(other.IsLive)
                 && (Odds.Except(other.Odds, new GenericComparer<OddDto>()).Count() == 0);

        }

        public override int GetHashCode()
        {
            return Id.GetHashCode()
             ^ Name.GetHashCode()
             ^ IsLive.GetHashCode()
             ^ Odds.Aggregate(0, (acc, x) => acc ^ x.GetHashCode());
        }
    }
}
