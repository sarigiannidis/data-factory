/*
* http://github.com/moodmosaic/Fare/
* Original Java code:
* http://www.brics.dk/automaton/
*
* Operations for building minimal deterministic automata from sets of strings.
* The algorithm requires sorted input data, but is very fast (nearly linear with the input size).
*
* @author Dawid Weiss
*/

namespace Df.Stochastic.Fare
{
    using System;
    using System.Collections.Generic;

    internal sealed class LexicographicComparer : IComparer<char[]>
    {
        public int Compare(char[] s1, char[] s2)
        {
            var lens1 = s1.Length;
            var lens2 = s2.Length;
            var max = Math.Min(lens1, lens2);

            for (var i = 0; i < max; i++)
            {
                var c1 = s1[i];
                var c2 = s2[i];
                if (c1 != c2)
                {
                    return c1 - c2;
                }
            }

            return lens1 - lens2;
        }
    }
}