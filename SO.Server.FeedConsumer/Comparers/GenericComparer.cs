using System;
using System.Collections.Generic;

namespace SO.Server.FeedConsumer.Comparers
{
    public class GenericComparer<T> : IEqualityComparer<T> where T : class, IEquatable<T>
    {
        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Equals(y);
        }

        public int GetHashCode(T obj)
        {
            if (ReferenceEquals(obj, null))
                return 0;

            return obj.GetHashCode();
        }
    }
}
