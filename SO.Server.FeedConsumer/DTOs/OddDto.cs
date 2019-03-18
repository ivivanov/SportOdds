using System;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.DTOs
{
    public class OddDto//: IComparable<double>
    {
        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public double Value { get; set; }

        //public int CompareTo(double other)
        //{
        //    return other.CompareTo(Value);  
        //}
    }
}
