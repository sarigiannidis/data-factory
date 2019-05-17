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
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    [ExcludeFromCodeCoverage]
    internal sealed partial class StringUnionOperations
    {
        private readonly State _Root = new State();

        private StringBuilder _Previous;

        private IDictionary<State, State> _Register = new Dictionary<State, State>();

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
                _Previous == null || new LexicographicComparer().Compare(_Previous.ToString().ToCharArray(), current) <= 0,
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