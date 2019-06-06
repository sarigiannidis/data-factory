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
    using System.Diagnostics;
    using System.Linq;

    internal sealed partial class StringUnionOperations
    {
        private sealed class State
        {
            private State[] _States = Array.Empty<State>();

            public bool HasChildren => TransitionLabels.Length > 0;

            public bool IsFinal { get; set; }

            public State LastChild
            {
                get
                {
                    Debug.Assert(HasChildren, "No outgoing transitions.");
                    return _States[^1];
                }
            }

            public IEnumerable<State> States => _States;

            public char[] TransitionLabels { get; private set; } = Array.Empty<char>();

            public override bool Equals(object obj) => obj is State other
                    && IsFinal == other.IsFinal
                    && ReferenceEquals(_States, other._States)
                    && Equals(TransitionLabels, other.TransitionLabels);

            public override int GetHashCode()
            {
                var hashCode = default(HashCode);
                hashCode.Add(IsFinal);
                hashCode.AddRange(TransitionLabels);
                hashCode.AddRange(_States);
                return hashCode.ToHashCode();
            }

            public State GetLastChild(char label)
            {
                var index = TransitionLabels.Length - 1;
                State s = null;
                if (index >= 0 && TransitionLabels[index] == label)
                {
                    s = _States[index];
                }

                Debug.Assert(s == GetState(label));
                return s;
            }

            public State NewState(char label)
            {
                Debug.Assert(Array.BinarySearch(TransitionLabels, label) < 0, "State already has transition labeled: " + label);
                TransitionLabels = CopyOf(TransitionLabels, TransitionLabels.Length + 1);
                _States = CopyOf(_States, _States.Length + 1);
                TransitionLabels[^1] = label;
                return _States[^1] = new State();
            }

            public void ReplaceLastChild(State state)
            {
                Debug.Assert(HasChildren, "No outgoing transitions.");
                _States[^1] = state;
            }

            private static char[] CopyOf(char[] original, int newLength)
            {
                var copy = new char[newLength];
                Array.Copy(original, 0, copy, 0, Math.Min(original.Length, newLength));
                return copy;
            }

            private static State[] CopyOf(State[] original, int newLength)
            {
                var copy = new State[newLength];
                Array.Copy(original, 0, copy, 0, Math.Min(original.Length, newLength));
                return copy;
            }

            private static bool ReferenceEquals(object[] a1, object[] a2) => a1.Length == a2.Length && !a1.Where((t, i) => t != a2[i]).Any();

            private State GetState(char label)
            {
                var index = Array.BinarySearch(TransitionLabels, label);
                return index >= 0 ? _States[index] : null;
            }
        }
    }
}