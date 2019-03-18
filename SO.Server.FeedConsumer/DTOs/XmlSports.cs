using System;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.DTOs
{
    [XmlRoot(ElementName = "XmlSports")]
    public class XmlSports : IEquatable<XmlSports>
    {
        [XmlAttribute]
        public DateTime CreateDate { get; set; }

        [XmlElement]
        public SportDto Sport { get; set; }

        public bool Equals(XmlSports other)
        {
            return CreateDate.Equals(other.CreateDate) && Sport.Equals(other.Sport);
        }

        public override int GetHashCode()
        {
            return CreateDate.GetHashCode() ^ Sport.GetHashCode();
        }
    }
}
