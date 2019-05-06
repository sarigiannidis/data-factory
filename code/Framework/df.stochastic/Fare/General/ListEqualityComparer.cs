namespace Df.Stochastic.Fare
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class ListEqualityComparer<T>
        : IEqualityComparer<List<T>>
    {
        public bool Equals(List<T> x, List<T> y)
        {
            return x.Count != y.Count ? false : x.SequenceEqual(y);
        }

        // http://stackoverflow.com/questions/1079192/is-it-possible-to-combine-hash-codes-for-private-members-to-generate-a-new-hash
        public int GetHashCode(List<T> obj) =>
            obj.Aggregate(17, (current, item) => (current * 31) + item.GetHashCode());
    }
}