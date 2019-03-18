using SO.Server.FeedConsumer.Compares;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.DTOs
{
    public class MatchDto : IEquatable<MatchDto>
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

        public bool Equals(MatchDto other)
        {
            bool areEqual = Id.Equals(other.Id)
                 && Name.Equals(other.Name)
                 && StartDate.Equals(other.StartDate)
                 && MatchType.Equals(other.MatchType);

            if (HasBets)
                return areEqual && (Bets.Except(other.Bets, new GenericComparer<BetDto>()).Count() == 0);

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
