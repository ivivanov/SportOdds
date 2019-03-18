using SO.Server.FeedConsumer.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

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

            for (int i = 0; i < 1000; i++)
            {

            var result = a.Sport.Events.Except(b.Sport.Events);
            }
            var bxcv = 1;
        }
    }
}
