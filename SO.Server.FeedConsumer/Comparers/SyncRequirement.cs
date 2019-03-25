using System.Collections.Generic;

namespace SO.Server.FeedConsumer.Comparers
{
    public class SyncRequirement
    {
        public IEnumerable<int> Add { get; set; }

        public IEnumerable<int> Update { get; set; }

        public IEnumerable<int> Delete { get; set; }
    }
}
