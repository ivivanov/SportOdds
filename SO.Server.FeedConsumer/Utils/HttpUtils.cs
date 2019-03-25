using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace SO.Server.FeedConsumer.Utils
{
    public static class HttpUtils
    {
        public static string SendHttpRequest(HttpWebRequest webRequest, string postData = null)
        {
            if (postData != null)
            {
                using (var writer = new StreamWriter(webRequest.GetRequestStream()))
                {
                    writer.Write(postData);
                }
            }

            try
            {
                using (var webResponse = webRequest.GetResponse())
                {
                    using (var str = webResponse.GetResponseStream())
                    {
                        using (var sr = new StreamReader(str))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                dynamic obj = JsonConvert.DeserializeObject(resp);
                throw;
            }
        }
    }
}
