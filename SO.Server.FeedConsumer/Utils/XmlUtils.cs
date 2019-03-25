using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SO.Server.FeedConsumer.Utils
{
    public class XmlUtils
    {
        public static T Deserialize<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            var model = (T)serializer.Deserialize(new StringReader(xml));
            return model;
        }
        public static string GetXmlString(string path)
        {
            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var stream = new StreamReader(fs, Encoding.UTF8);
                return stream.ReadToEnd();
            }
        }
    }
}
