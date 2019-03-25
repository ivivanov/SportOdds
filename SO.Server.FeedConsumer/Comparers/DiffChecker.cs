using SO.Server.FeedConsumer.Models;
using System.Collections.Generic;
using System.Linq;

namespace SO.Server.FeedConsumer.Comparers
{
    public class DiffChecker
    {
        public static IEnumerable<int> ToDeleteIDs<T>(IEnumerable<T> current, IEnumerable<T> updated)
            where T : IBaseModel
        {
            return current.Select(x => x.Id).Except(updated.Select(x => x.Id));
        }

        public static IEnumerable<int> ToAddIDs<T>(IEnumerable<T> current, IEnumerable<T> updated)
            where T : IBaseModel
        {
            return updated.Select(x => x.Id).Except(current.Select(x => x.Id));
        }

        public static IEnumerable<int> ToUpdateIDs<T>(IEnumerable<T> current, IEnumerable<T> updated)
            where T : IBaseModel
        {
            return current
                .Except(updated)
                .Select(x => x.Id)
                .Except(
                    ToDeleteIDs(current, updated)
                    .Union(ToAddIDs(current, updated))
                );
        }

        public static SyncRequirement GetSyncRequirement<T>(IEnumerable<T> oldCollection, IEnumerable<T> newCollection)
           where T : IBaseModel
        {
            return new SyncRequirement()
            {
                Add = ToAddIDs(oldCollection, newCollection),
                Update = ToUpdateIDs(oldCollection, newCollection),
                Delete = ToDeleteIDs(oldCollection, newCollection)
            };
        }
    }
}
