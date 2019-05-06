namespace Df.Stochastic.Fare
{
    using System.Collections.Generic;

    internal static class LinkedListExtensions
    {
        public static T RemoveAndReturnFirst<T>(this LinkedList<T> linkedList)
        {
            var first = linkedList.First.Value;
            linkedList.RemoveFirst();
            return first;
        }
    }
}