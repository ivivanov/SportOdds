using System.Collections.Generic;
using System.Linq;

namespace SO.Server.FeedConsumer.Comparers
{
    public class SyncRequirement
    {
        public IEnumerable<int> Add { get; set; }

        public IEnumerable<int> Update { get; set; }

        public IEnumerable<int> Delete { get; set; }

        public bool IsSyncRequired
        {
            get {
                return Add.Count() > 0 || Delete.Count() > 0 || Update.Count() > 0;
            }
        }
    }
}
