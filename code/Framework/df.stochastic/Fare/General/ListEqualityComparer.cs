namespace Df.Stochastic.Fare
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class ListEqualityComparer<T>
        : IEqualityComparer<List<T>>
    {
        public bool Equals(List<T> first, List<T> second) =>
            first.Count == second.Count && first.SequenceEqual(second);

        public int GetHashCode(List<T> list)
        {
            var hashCode = default(HashCode);
            hashCode.AddRange(list);
            return hashCode.ToHashCode();
        }
    }
}