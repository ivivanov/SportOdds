using System;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.Models
{
    public class OddModel : BaseModel, IEquatable<OddModel>
    {
        [XmlAttribute]
        public double Value { get; set; }

        public bool Equals(OddModel other)
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
