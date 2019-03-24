using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.Models
{
    public class BaseModel : IBaseModel
    {
        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }
    }
}
