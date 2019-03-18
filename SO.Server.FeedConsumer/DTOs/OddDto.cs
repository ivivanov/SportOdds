using System;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.DTOs
{
    public class OddDto : IEquatable<OddDto>
    {
        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public double Value { get; set; }

        public bool Equals(OddDto other)
        {
            return Id.Equals(other.Id)
                 && Name.Equals(other.Name)
                 && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode()
             ^ Name.GetHashCode()
             ^ Value.GetHashCode();
        }
    }
}
