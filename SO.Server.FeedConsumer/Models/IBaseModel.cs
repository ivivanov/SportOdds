using System;

namespace SO.Server.FeedConsumer.Models
{
    public interface IBaseModel
    {
        int Id { get; set; }

        string Name { get; set; }
    }
}
