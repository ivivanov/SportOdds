using SO.Server.FeedConsumer.Comparers;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.Models
{
    public class MatchModel : BaseModel, IEquatable<MatchModel>
    {
        [XmlAttribute]
        public DateTime StartDate { get; set; }

        [XmlAttribute]
        public string MatchType { get; set; }

        [XmlElement(ElementName = "Bet")]
        public BetModel[] Bets { get; set; }

        public bool Equals(MatchModel other)
        {
            bool areEqual = Id.Equals(other.Id)
                 && Name.Equals(other.Name)
                 && StartDate.Equals(other.StartDate)
                 && MatchType.Equals(other.MatchType);

            if (HasBets)
                return areEqual && (Bets.Except(other.Bets, new GenericComparer<BetModel>()).Count() == 0);

            return areEqual;
        }

        public override int GetHashCode()
        {
            int hash = Id.GetHashCode()
             ^ Name.GetHashCode()
             ^ StartDate.GetHashCode()
             ^ MatchType.GetHashCode();

            if (HasBets)
                return hash ^ Bets.Aggregate(0, (acc, x) => acc ^ x.GetHashCode());

            return hash;
        }

        private bool HasBets { get { return Bets != null && Bets.Count() > 0; } }

    }
}
