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
    using System.Runtime.CompilerServices;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal sealed class StringUnionOperations
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

            public override bool Equals(object obj) =>
                obj is State other
                    && IsFinal == other.IsFinal
                    && ReferenceEquals(_States, other._States)
                    && Equals(TransitionLabels, other.TransitionLabels);

            public override int GetHashCode()
            {
                var hash = IsFinal ? 1 : 0;
                hash ^= (hash * 31) + TransitionLabels.Length;
                hash = TransitionLabels.Aggregate(hash, (current, c) => current ^ ((current * 31) + c));

                // Compare the right-language of this state using reference-identity of outgoing
                // states. This is possible because states are interned (stored in registry) and
                // traversed in post-order, so any outgoing transitions are already interned.
                return _States.Aggregate(hash, (current, s) => current ^ RuntimeHelpers.GetHashCode(s));
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
                Debug.Assert(
                    Array.BinarySearch(TransitionLabels, label) < 0,
                    "State already has transition labeled: " + label);

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

            private static bool ReferenceEquals(object[] a1, object[] a2) =>
                a1.Length == a2.Length && !a1.Where((t, i) => t != a2[i]).Any();

            private State GetState(char label)
            {
                var index = Array.BinarySearch(TransitionLabels, label);
                return index >= 0 ? _States[index] : null;
            }
        }

        private readonly State _Root = new State();

        private StringBuilder _Previous;

        private IDictionary<State, State> _Register = new Dictionary<State, State>();

        public static IComparer<char[]> LexicographicOrderComparer { get; } = new LexicographicComparer();

        public static Fare.State Build(IEnumerable<char[]> input)
        {
            var builder = new StringUnionOperations();

            foreach (var chs in input)
            {
                builder.Add(chs);
            }

            return Convert(builder.Complete(), new Dictionary<State, Fare.State>());
        }

        public void Add(char[] current)
        {
            Debug.Assert(_Register != null, "Automaton already built.");
            Debug.Assert(current.Length > 0, "Input sequences must not be empty.");
            Debug.Assert(
                _Previous == null || LexicographicOrderComparer.Compare(_Previous.ToString().ToCharArray(), current) <= 0,
                "Input must be sorted: " + _Previous + " >= " + current);
            Debug.Assert(SetPrevious(current));

            // Descend in the automaton (find matching prefix).
            var pos = 0;
            var max = current.Length;
            State next;
            var state = _Root;
            while (pos < max && (next = state.GetLastChild(current[pos])) != null)
            {
                state = next;
                pos++;
            }

            if (state.HasChildren)
            {
                ReplaceOrRegister(state);
            }

            AddSuffix(state, current, pos);
        }

        private static void AddSuffix(State state, char[] current, int fromIndex)
        {
            for (var i = fromIndex; i < current.Length; i++)
            {
                state = state.NewState(current[i]);
            }

            state.IsFinal = true;
        }

        private static Fare.State Convert(State s, IDictionary<State, Fare.State> visited)
        {
            var converted = visited[s];
            if (converted != null)
            {
                return converted;
            }

            converted = new Fare.State
            {
                Accept = s.IsFinal,
            };

            visited.Add(s, converted);
            var i = 0;
            var labels = s.TransitionLabels;
            foreach (var target in s.States)
            {
                converted.AddTransition(new Transition(labels[i++], Convert(target, visited)));
            }

            return converted;
        }

        private State Complete()
        {
            if (_Register == null)
            {
                throw new InvalidOperationException("register is null");
            }

            if (_Root.HasChildren)
            {
                ReplaceOrRegister(_Root);
            }

            _Register = null;
            return _Root;
        }

        private void ReplaceOrRegister(State state)
        {
            var child = state.LastChild;

            if (child.HasChildren)
            {
                ReplaceOrRegister(child);
            }

            var registered = _Register[child];
            if (registered != null)
            {
                state.ReplaceLastChild(registered);
            }
            else
            {
                _Register.Add(child, child);
            }
        }

        private bool SetPrevious(char[] current)
        {
            _Previous ??= new StringBuilder();
            _Previous.Length = 0;
            _ = _Previous.Append(current);

            return true;
        }
    }
}