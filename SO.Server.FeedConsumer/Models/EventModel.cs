using SO.Server.FeedConsumer.Comparers;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.Models
{
    public class EventModel : BaseModel, IEquatable<EventModel>
    {
        [XmlAttribute]
        public bool IsLive { get; set; }

        [XmlElement(ElementName = "Match")]
        public MatchModel[] Matches { get; set; }

        public bool Equals(EventModel other)
        {
            return Id.Equals(other.Id)
                 && Name.Equals(other.Name)
                 && (Matches.Except(other.Matches, new GenericComparer<MatchModel>()).Count() == 0);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode()
             ^ Name.GetHashCode()
             ^ Matches.Aggregate(0, (acc, x) => acc ^ x.GetHashCode());
        }
    }
}
