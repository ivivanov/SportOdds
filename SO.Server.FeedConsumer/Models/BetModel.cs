using SO.Server.FeedConsumer.Comparers;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.Models
{
    public class BetModel : BaseModel, IEquatable<BetModel>
    {
        [XmlAttribute]
        public bool IsLive { get; set; }

        [XmlElement(ElementName = "Odd")]
        public OddModel[] Odds { get; set; }

        public bool Equals(BetModel other)
        {
            return Id.Equals(other.Id)
                 && Name.Equals(other.Name)
                 && IsLive.Equals(other.IsLive)
                 && (Odds.Except(other.Odds, new GenericComparer<OddModel>()).Count() == 0);
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
