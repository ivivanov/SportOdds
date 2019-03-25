using SO.Server.FeedConsumer.Models;
using SO.Server.FeedConsumer.Utils;
using System.Net;

namespace SO.Server.FeedConsumer.Feeds
{
    public class UltraplayFeedClient : IFeed
    {
        private const string baseUrl = "https://sports.ultraplay.net";
        private const string clientKey = "b4dde172-4e11-43e4-b290-abdeb0ffd711";
        
        public XmlSportsModel GetSports(int sportId)
        {
            return XmlSportsModel.Create(QueryPrivate($"sportsxml?sportId={sportId}"));
        }

        private string QueryPrivate(string methodName)
        {
            var address = $"{baseUrl}/{methodName}&clientKey={clientKey}";
            var webRequest = (HttpWebRequest)WebRequest.Create(address);
            webRequest.Method = "GET";

            return HttpUtils.SendHttpRequest(webRequest);
        }
    }
}
