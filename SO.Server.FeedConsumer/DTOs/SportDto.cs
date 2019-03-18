using SO.Server.FeedConsumer.Comparers;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.DTOs
{
    public class SportDto : IEquatable<SportDto>
    {
        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement(ElementName = "Event")]
        public EventDto[] Events { get; set; }

        public bool Equals(SportDto other)
        {
            return Id.Equals(other.Id)
                 && Name.Equals(other.Name)
                 && (Events.Except(other.Events, new GenericComparer<EventDto>()).Count() == 0);
        }

        public override int GetHashCode()
        {

            return Id.GetHashCode()
                ^ Name.GetHashCode()
                ^ Events.Aggregate(0, (acc, x) => acc ^ x.GetHashCode());
        }

    }
}
