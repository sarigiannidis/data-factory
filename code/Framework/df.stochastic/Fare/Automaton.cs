/*
* dk.brics.automaton
*
* Copyright (c) 2001-2011 Anders Moeller
* All rights reserved.
* http://github.com/moodmosaic/Fare/
* Original Java code:
* http://www.brics.dk/automaton/
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions
* are met:
* 1. Redistributions of source code must retain the above copyright
*    notice, this list of conditions and the following disclaimer.
* 2. Redistributions in binary form must reproduce the above copyright
*    notice, this list of conditions and the following disclaimer in the
*    documentation and/or other materials provided with the distribution.
* 3. The name of the author may not be used to endorse or promote products
*    derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
* IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
* OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
* IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
* INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
* NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
* DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
* THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
* THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

namespace Df.Stochastic.Fare
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    [ExcludeFromCodeCoverage]
    internal class Automaton
    {
        public const int MinimizeBrzozowski = 1;

        public const int MinimizeHopcroft = 2;

        public const int MinimizeHuffman = 0;

        private static bool _AllowMutation;

        private static bool _MinimizeAlways;

        private int _HashCode;

        private State _Initial = new State();

        public static bool AllowMutation { get; set; }

        public static int Minimization => MinimizeHopcroft;

        public State Initial
        {
            get
            {
                ExpandSingleton();
                return _Initial;
            }

            set
            {
                Singleton = null;
                _Initial = value;
            }
        }

        public bool IsDebug { get; set; }

        public bool IsDeterministic { get; set; } = true;

        public bool IsEmpty { get; set; }

        public bool IsSingleton => Singleton != null;

        public int NumberOfStates => IsSingleton ? Singleton.Length + 1 : GetStates().Count;

        public int NumberOfTransitions => IsSingleton ? Singleton.Length : GetStates().Sum(_ => _.Transitions.Count);

        public string Singleton { get; set; }

        public static Transition[][] GetSortedTransitions(HashSet<State> states)
        {
            SetStateNumbers(states);
            var transitions = new Transition[states.Count][];
            foreach (var s in states)
            {
                transitions[s.Number] = s.GetSortedTransitions(false).ToArray();
            }

            return transitions;
        }

        public static Automaton MakeChar(char c) => BasicAutomata.MakeChar(c);

        public static Automaton MakeCharSet(string set) => BasicAutomata.MakeCharSet(set);

        public static Automaton MakeString(string s) => BasicAutomata.MakeString(s);

        public static Automaton Minimize(Automaton a)
        {
            a.Minimize();
            return a;
        }

        public static bool SetAllowMutate(bool flag)
        {
            var b = _AllowMutation;
            _AllowMutation = flag;
            return b;
        }

        public static void SetMinimizeAlways(bool flag) => _MinimizeAlways = flag;

        public static void SetStateNumbers(IEnumerable<State> states)
        {
            var number = 0;
            foreach (var s in states)
            {
                s.Number = number++;
            }
        }

        public void AddEpsilons(ICollection<StatePair> pairs) => BasicOperations.AddEpsilons(this, pairs);

        public void CheckMinimizeAlways()
        {
            if (_MinimizeAlways)
            {
                Minimize();
            }
        }

        public void ClearHashCode() => _HashCode = 0;

        public Automaton Clone()
        {
            var a = (Automaton)MemberwiseClone();
            if (!IsSingleton)
            {
                var states = GetStates();
                var d = states.ToDictionary(_ => _, _ => new State());

                foreach (var s in states)
                {
                    if (!d.TryGetValue(s, out var p))
                    {
                        continue;
                    }

                    p.Accept = s.Accept;
                    if (s == Initial)
                    {
                        a.Initial = p;
                    }

                    foreach (var t in s.Transitions)
                    {
                        _ = d.TryGetValue(t.To, out var to);
                        p.Transitions.Add(new Transition(t.Min, t.Max, to));
                    }
                }
            }

            return a;
        }

        public Automaton CloneExpanded()
        {
            var a = Clone();
            a.ExpandSingleton();
            return a;
        }

        public Automaton CloneExpandedIfRequired()
        {
            if (AllowMutation)
            {
                ExpandSingleton();
                return this;
            }

            return CloneExpanded();
        }

        public Automaton CloneIfRequired() => _AllowMutation ? this : Clone();

        public Automaton Complement() => BasicOperations.Complement(this);

        public Automaton Concatenate(Automaton a) => BasicOperations.Concatenate(this, a);

        public void Determinize() => BasicOperations.Determinize(this);

        public void ExpandSingleton()
        {
            if (IsSingleton)
            {
                var p = new State();
                _Initial = p;
                foreach (var t in Singleton)
                {
                    var q = new State();
                    p.Transitions.Add(new Transition(t, q));
                    p = q;
                }

                p.Accept = true;
                IsDeterministic = true;
                Singleton = null;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is not executing immediately nor returns the same value each time it is invoked.")]
        public HashSet<State> GetAcceptStates()
        {
            ExpandSingleton();

            var accepts = new HashSet<State>();
            var visited = new HashSet<State>();

            var worklist = new LinkedList<State>();
            _ = worklist.AddLast(Initial);

            _ = visited.Add(Initial);

            while (worklist.Count > 0)
            {
                var s = worklist.RemoveAndReturnFirst();
                if (s.Accept)
                {
                    _ = accepts.Add(s);
                }

                foreach (var t in s.Transitions)
                {
                    if (t.To == null)
                    {
                        continue;
                    }

                    if (!visited.Contains(t.To))
                    {
                        _ = visited.Add(t.To);
                        _ = worklist.AddLast(t.To);
                    }
                }
            }

            return accepts;
        }

        public override int GetHashCode()
        {
            if (_HashCode == 0)
            {
                Minimize();
            }

            return _HashCode;
        }

        public HashSet<State> GetLiveStates()
        {
            ExpandSingleton();
            return GetLiveStates(GetStates());
        }

        public char[] GetStartPoints()
        {
            var pointSet = new HashSet<char>();
            foreach (var s in GetStates())
            {
                _ = pointSet.Add(char.MinValue);
                foreach (var t in s.Transitions)
                {
                    _ = pointSet.Add(t.Min);
                    if (t.Max < char.MaxValue)
                    {
                        _ = pointSet.Add((char)(t.Max + 1));
                    }
                }
            }

            var points = new char[pointSet.Count];
            var n = 0;
            foreach (var m in pointSet)
            {
                points[n++] = m;
            }

            Array.Sort(points);
            return points;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is not executing immediately nor returns the same value each time it is invoked.")]
        public HashSet<State> GetStates()
        {
            ExpandSingleton();
            var visited = new HashSet<State>();
            var worklist = new LinkedList<State>();
            _ = worklist.AddLast(Initial);
            _ = visited.Add(Initial);
            while (worklist.Count > 0)
            {
                var s = worklist.RemoveAndReturnFirst();
                if (s == null)
                {
                    continue;
                }

                var tr = IsDebug
                    ? new HashSet<Transition>(s.GetSortedTransitions(false))
                    : new HashSet<Transition>(s.Transitions);

                foreach (var t in tr)
                {
                    if (!visited.Contains(t.To))
                    {
                        _ = visited.Add(t.To);
                        _ = worklist.AddLast(t.To);
                    }
                }
            }

            return visited;
        }

        public Automaton Intersection(Automaton a) => BasicOperations.Intersection(this, a);

        public bool IsEmptyString() => BasicOperations.IsEmptyString(this);

        public void Minimize() => MinimizationOperations.Minimize(this);

        public Automaton Optional() => BasicOperations.Optional(this);

        public void RecomputeHashCode()
        {
            _HashCode = (NumberOfStates * 3) + (NumberOfTransitions * 2);
            if (_HashCode == 0)
            {
                _HashCode = 1;
            }
        }

        public void Reduce()
        {
            if (IsSingleton)
            {
                return;
            }

            var states = GetStates();
            SetStateNumbers(states);
            foreach (var s in states)
            {
                var st = s.GetSortedTransitions(true);
                s.ResetTransitions();
                State p = null;
                int min = -1, max = -1;
                foreach (var t in st)
                {
                    if (p == t.To)
                    {
                        if (t.Min <= max + 1)
                        {
                            if (t.Max > max)
                            {
                                max = t.Max;
                            }
                        }
                        else
                        {
                            if (p != null)
                            {
                                s.Transitions.Add(new Transition((char)min, (char)max, p));
                            }

                            min = t.Min;
                            max = t.Max;
                        }
                    }
                    else
                    {
                        if (p != null)
                        {
                            s.Transitions.Add(new Transition((char)min, (char)max, p));
                        }

                        p = t.To;
                        min = t.Min;
                        max = t.Max;
                    }
                }

                if (p != null)
                {
                    s.Transitions.Add(new Transition((char)min, (char)max, p));
                }
            }

            ClearHashCode();
        }

        public void RemoveDeadTransitions()
        {
            ClearHashCode();
            if (IsSingleton)
            {
                return;
            }

            var states = new HashSet<State>(GetStates().Where(state => state != null));
            var live = GetLiveStates(states);
            foreach (var s in states)
            {
                var st = s.Transitions;
                s.ResetTransitions();
                foreach (var t in st)
                {
                    if (t.To == null)
                    {
                        continue;
                    }

                    if (live.Contains(t.To))
                    {
                        s.Transitions.Add(t);
                    }
                }
            }

            Reduce();
        }

        public Automaton Repeat(int min, int max) => BasicOperations.Repeat(this, min, max);

        public Automaton Repeat() => BasicOperations.Repeat(this);

        public Automaton Repeat(int min) => BasicOperations.Repeat(this, min);

        public bool Run(string s) => BasicOperations.Run(this, s);

        public void Totalize()
        {
            var s = new State();
            s.Transitions.Add(new Transition(char.MinValue, char.MaxValue, s));

            foreach (var p in GetStates())
            {
                int maxi = char.MinValue;
                foreach (var t in p.GetSortedTransitions(false))
                {
                    if (t.Min > maxi)
                    {
                        p.Transitions.Add(new Transition((char)maxi, (char)(t.Min - 1), s));
                    }

                    if (t.Max + 1 > maxi)
                    {
                        maxi = t.Max + 1;
                    }
                }

                if (maxi <= char.MaxValue)
                {
                    p.Transitions.Add(new Transition((char)maxi, char.MaxValue, s));
                }
            }
        }

        private HashSet<State> GetLiveStates(HashSet<State> states)
        {
            var dictionary = states.ToDictionary(_ => _, _ => new HashSet<State>());

            foreach (var s in states)
            {
                foreach (var t in s.Transitions)
                {
                    if (t.To == null)
                    {
                        continue;
                    }

                    _ = dictionary[t.To].Add(s);
                }
            }

            var live = new HashSet<State>(GetAcceptStates());
            var worklist = new LinkedList<State>(live);
            while (worklist.Count > 0)
            {
                var s = worklist.RemoveAndReturnFirst();
                foreach (var p in dictionary[s])
                {
                    if (!live.Contains(p))
                    {
                        _ = live.Add(p);
                        _ = worklist.AddLast(p);
                    }
                }
            }

            return live;
        }
    }
}