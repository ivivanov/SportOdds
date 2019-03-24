using SO.Server.FeedConsumer.Comparers;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.Models
{
    public class SportModel : BaseModel, IEquatable<SportModel>
    {
        [XmlElement(ElementName = "Event")]
        public EventModel[] Events { get; set; }

        public bool Equals(SportModel other)
        {
            return Id.Equals(other.Id)
                 && Name.Equals(other.Name)
                 && (Events.Except(other.Events, new GenericComparer<EventModel>()).Count() == 0);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode()
                ^ Name.GetHashCode()
                ^ Events.Aggregate(0, (acc, x) => acc ^ x.GetHashCode());
        }
    }
}
