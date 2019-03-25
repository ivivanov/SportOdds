using SO.Server.FeedConsumer.Models;

namespace SO.Server.FeedConsumer.Feeds
{
    public interface IFeed
    {
        XmlSportsModel GetSports(int sportId);
    }
}
