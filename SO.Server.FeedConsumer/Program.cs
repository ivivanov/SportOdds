using SO.Server.FeedConsumer.DTOs;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

namespace SO.Server.FeedConsumer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var doc = new XmlDocument();
            doc.Load("soccer.xml");

            var serializer = new XmlSerializer(typeof(XmlSports));

            XmlSports a;

            using (var reader = new StringReader(doc.InnerXml))
            {
                a = (XmlSports)serializer.Deserialize(reader);
            }


            var doc2 = new XmlDocument();
            doc2.Load("soccer2.xml");

            var serializer2 = new XmlSerializer(typeof(XmlSports));
            XmlSports b;
            using (var reader = new StringReader(doc2.InnerXml))
            {
                b = (XmlSports)serializer2.Deserialize(reader);
            }

            var diff = b.Sport.Events.SequenceEqual(a.Sport.Events);

            //UnitOfWork uow = new UnitOfWork<>
        }
    }
}
