﻿/*
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    [ExcludeFromCodeCoverage]
    internal static class BasicOperations
    {
        public static void AddEpsilons(Automaton a, ICollection<StatePair> pairs)
        {
            a.ExpandSingleton();
            var forward = new Dictionary<State, HashSet<State>>();
            var back = new Dictionary<State, HashSet<State>>();
            foreach (var p in pairs)
            {
                var to = forward[p.FirstState];
                if (to == null)
                {
                    to = new HashSet<State>();
                    forward.Add(p.FirstState, to);
                }

                _ = to.Add(p.SecondState);
                var from = back[p.SecondState];
                if (from == null)
                {
                    from = new HashSet<State>();
                    back.Add(p.SecondState, from);
                }

                _ = from.Add(p.FirstState);
            }

            var worklist = new LinkedList<StatePair>(pairs);
            var workset = new HashSet<StatePair>(pairs);
            while (worklist.Count != 0)
            {
                var p = worklist.RemoveAndReturnFirst();
                _ = workset.Remove(p);
                var to = forward[p.SecondState];
                var from = back[p.FirstState];
                if (to != null)
                {
                    foreach (var s in to)
                    {
                        var pp = new StatePair(p.FirstState, s);
                        if (!pairs.Contains(pp))
                        {
                            pairs.Add(pp);
                            _ = forward[p.FirstState].Add(s);
                            _ = back[s].Add(p.FirstState);
                            _ = worklist.AddLast(pp);
                            _ = workset.Add(pp);
                            if (from != null)
                            {
                                foreach (var q in from)
                                {
                                    var qq = new StatePair(q, p.FirstState);
                                    if (!workset.Contains(qq))
                                    {
                                        _ = worklist.AddLast(qq);
                                        _ = workset.Add(qq);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (var p in pairs)
            {
                p.FirstState.AddEpsilon(p.SecondState);
            }

            a.IsDeterministic = false;
            a.ClearHashCode();
            a.CheckMinimizeAlways();
        }

        public static Automaton Complement(Automaton a)
        {
            a = a.CloneExpandedIfRequired();
            a.Determinize();
            a.Totalize();
            foreach (var p in a.GetStates())
            {
                p.Accept = !p.Accept;
            }

            a.RemoveDeadTransitions();
            return a;
        }

        public static Automaton Concatenate(Automaton a1, Automaton a2)
        {
            if (a1.IsSingleton && a2.IsSingleton)
            {
                return BasicAutomata.MakeString(a1.Singleton + a2.Singleton);
            }

            if (IsEmpty(a1) || IsEmpty(a2))
            {
                return BasicAutomata.MakeEmpty();
            }

            var deterministic = a1.IsSingleton && a2.IsDeterministic;
            if (a1 == a2)
            {
                a1 = a1.CloneExpanded();
                a2 = a2.CloneExpanded();
            }
            else
            {
                a1 = a1.CloneExpandedIfRequired();
                a2 = a2.CloneExpandedIfRequired();
            }

            foreach (var s in a1.GetAcceptStates())
            {
                s.Accept = false;
                s.AddEpsilon(a2.Initial);
            }

            a1.IsDeterministic = deterministic;
            a1.ClearHashCode();
            a1.CheckMinimizeAlways();
            return a1;
        }

        public static Automaton Concatenate(IList<Automaton> l)
        {
            if (l.Count == 0)
            {
                return BasicAutomata.MakeEmptyString();
            }

            var allSingleton = l.All(_ => _.IsSingleton);

            if (allSingleton)
            {
                var b = new StringBuilder();
                foreach (var a in l)
                {
                    _ = b.Append(a.Singleton);
                }

                return BasicAutomata.MakeString(b.ToString());
            }
            else
            {
                if (l.Any(_ => _.IsEmpty))
                {
                    return BasicAutomata.MakeEmpty();
                }

                var ids = new HashSet<int>();
                foreach (var a in l)
                {
                    _ = ids.Add(RuntimeHelpers.GetHashCode(a));
                }

                var hasAliases = ids.Count != l.Count;
                var b = l[0];
                b = hasAliases ? b.CloneExpanded() : b.CloneExpandedIfRequired();

                var ac = b.GetAcceptStates();
                var first = true;
                foreach (var a in l)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        if (a.IsEmptyString())
                        {
                            continue;
                        }

                        var aa = a;
                        aa = hasAliases ? aa.CloneExpanded() : aa.CloneExpandedIfRequired();

                        var ns = aa.GetAcceptStates();
                        foreach (var s in ac)
                        {
                            s.Accept = false;
                            s.AddEpsilon(aa.Initial);
                            if (s.Accept)
                            {
                                _ = ns.Add(s);
                            }
                        }

                        ac = ns;
                    }
                }

                b.IsDeterministic = false;
                b.ClearHashCode();
                b.CheckMinimizeAlways();
                return b;
            }
        }

        public static void Determinize(Automaton a)
        {
            if (a.IsDeterministic || a.IsSingleton)
            {
                return;
            }

            var initialset = new HashSet<State>
            {
                a.Initial,
            };
            Determinize(a, initialset.ToList());
        }

        public static void Determinize(Automaton a, List<State> initialset)
        {
            var points = a.GetStartPoints();
            var comparer = new ListEqualityComparer<State>();

            var sets = new Dictionary<List<State>, List<State>>(comparer);
            var worklist = new LinkedList<List<State>>();
            var newstate = new Dictionary<List<State>, State>(comparer);

            sets.Add(initialset, initialset);
            _ = worklist.AddLast(initialset);
            a.Initial = new State();
            newstate.Add(initialset, a.Initial);

            while (worklist.Count > 0)
            {
                var s = worklist.RemoveAndReturnFirst();
                _ = newstate.TryGetValue(s, out var r);
                foreach (var q in s)
                {
                    if (q.Accept)
                    {
                        r.Accept = true;
                        break;
                    }
                }

                for (var n = 0; n < points.Length; n++)
                {
                    var set = new HashSet<State>();
                    foreach (var c in s)
                    {
                        foreach (var t in c.Transitions)
                        {
                            if (t.Min <= points[n] && points[n] <= t.Max)
                            {
                                _ = set.Add(t.To);
                            }
                        }
                    }

                    var p = set.ToList();

                    if (!sets.ContainsKey(p))
                    {
                        sets.Add(p, p);
                        _ = worklist.AddLast(p);
                        newstate.Add(p, new State());
                    }

                    _ = newstate.TryGetValue(p, out var q);
                    var min = points[n];
                    var max = n + 1 < points.Length ? (char)(points[n + 1] - 1) : char.MaxValue;
                    r.Transitions.Add(new Transition(min, max, q));
                }
            }

            a.IsDeterministic = true;
            a.RemoveDeadTransitions();
        }

        public static Automaton Intersection(Automaton a1, Automaton a2)
        {
            if (a1.IsSingleton)
            {
                return a2.Run(a1.Singleton) ? a1.CloneIfRequired() : BasicAutomata.MakeEmpty();
            }

            if (a2.IsSingleton)
            {
                return a1.Run(a2.Singleton) ? a2.CloneIfRequired() : BasicAutomata.MakeEmpty();
            }

            if (a1 == a2)
            {
                return a1.CloneIfRequired();
            }

            var transitions1 = Automaton.GetSortedTransitions(a1.GetStates());
            var transitions2 = Automaton.GetSortedTransitions(a2.GetStates());
            var c = new Automaton();
            var worklist = new LinkedList<StatePair>();
            var newstates = new Dictionary<StatePair, StatePair>();
            var p = new StatePair(c.Initial, a1.Initial, a2.Initial);
            _ = worklist.AddLast(p);
            newstates.Add(p, p);
            while (worklist.Count > 0)
            {
                p = worklist.RemoveAndReturnFirst();
                p.S.Accept = p.FirstState.Accept && p.SecondState.Accept;
                var t1 = transitions1[p.FirstState.Number];
                var t2 = transitions2[p.SecondState.Number];
                for (int n1 = 0, b2 = 0; n1 < t1.Length; n1++)
                {
                    while (b2 < t2.Length && t2[b2].Max < t1[n1].Min)
                    {
                        b2++;
                    }

                    for (var n2 = b2; n2 < t2.Length && t1[n1].Max >= t2[n2].Min; n2++)
                    {
                        if (t2[n2].Max >= t1[n1].Min)
                        {
                            var q = new StatePair(t1[n1].To, t2[n2].To);
                            _ = newstates.TryGetValue(q, out var r);
                            if (r == null)
                            {
                                q.S = new State();
                                _ = worklist.AddLast(q);
                                newstates.Add(q, q);
                                r = q;
                            }

                            var min = t1[n1].Min > t2[n2].Min ? t1[n1].Min : t2[n2].Min;
                            var max = t1[n1].Max < t2[n2].Max ? t1[n1].Max : t2[n2].Max;
                            p.S.Transitions.Add(new Transition(min, max, r.S));
                        }
                    }
                }
            }

            c.IsDeterministic = a1.IsDeterministic && a2.IsDeterministic;
            c.RemoveDeadTransitions();
            c.CheckMinimizeAlways();
            return c;
        }

        public static bool IsEmpty(Automaton a) => !a.IsSingleton
                && !a.Initial.Accept
                && a.Initial.Transitions.Count == 0;

        public static bool IsEmptyString(Automaton a) => a.IsSingleton
                ? a.Singleton.Length == 0
                : a.Initial.Accept && a.Initial.Transitions.Count == 0;

        public static Automaton Optional(Automaton a)
        {
            a = a.CloneExpandedIfRequired();
            var s = new State();
            s.AddEpsilon(a.Initial);
            s.Accept = true;
            a.Initial = s;
            a.IsDeterministic = false;
            a.ClearHashCode();
            a.CheckMinimizeAlways();
            return a;
        }

        public static Automaton Repeat(Automaton a)
        {
            a = a.CloneExpanded();
            var s = new State
            {
                Accept = true,
            };
            s.AddEpsilon(a.Initial);
            foreach (var p in a.GetAcceptStates())
            {
                p.AddEpsilon(s);
            }

            a.Initial = s;
            a.IsDeterministic = false;
            a.ClearHashCode();
            a.CheckMinimizeAlways();
            return a;
        }

        public static Automaton Repeat(Automaton a, int min)
        {
            if (min == 0)
            {
                return Repeat(a);
            }

            var @as = new List<Automaton>();
            while (min-- > 0)
            {
                @as.Add(a);
            }

            @as.Add(Repeat(a));
            return Concatenate(@as);
        }

        public static Automaton Repeat(Automaton a, int min, int max)
        {
            if (min > max)
            {
                return BasicAutomata.MakeEmpty();
            }

            max -= min;
            a.ExpandSingleton();
            Automaton b;
            if (min == 0)
            {
                b = BasicAutomata.MakeEmptyString();
            }
            else if (min == 1)
            {
                b = a.Clone();
            }
            else
            {
                var @as = new List<Automaton>();
                while (min-- > 0)
                {
                    @as.Add(a);
                }

                b = Concatenate(@as);
            }

            if (max > 0)
            {
                var d = a.Clone();
                while (--max > 0)
                {
                    var c = a.Clone();
                    foreach (var p in c.GetAcceptStates())
                    {
                        p.AddEpsilon(d.Initial);
                    }

                    d = c;
                }

                foreach (var p in b.GetAcceptStates())
                {
                    p.AddEpsilon(d.Initial);
                }

                b.IsDeterministic = false;
                b.ClearHashCode();
                b.CheckMinimizeAlways();
            }

            return b;
        }

        public static bool Run(Automaton a, string s)
        {
            if (a.IsSingleton)
            {
                return s.Equals(a.Singleton);
            }

            if (a.IsDeterministic)
            {
                var p = a.Initial;
                foreach (var t in s)
                {
                    var q = p.Step(t);
                    if (q == null)
                    {
                        return false;
                    }

                    p = q;
                }

                return p.Accept;
            }

            var states = a.GetStates();
            Automaton.SetStateNumbers(states);
            var pp = new LinkedList<State>();
            var ppOther = new LinkedList<State>();
            var bb = new BitArray(states.Count);
            var bbOther = new BitArray(states.Count);
            _ = pp.AddLast(a.Initial);
            var dest = new List<State>();
            var accept = a.Initial.Accept;

            foreach (var c in s)
            {
                accept = false;
                ppOther.Clear();
                bbOther.SetAll(false);
                foreach (var p in pp)
                {
                    dest.Clear();
                    p.Step(c, dest);
                    foreach (var q in dest)
                    {
                        if (q.Accept)
                        {
                            accept = true;
                        }

                        if (!bbOther.Get(q.Number))
                        {
                            bbOther.Set(q.Number, true);
                            _ = ppOther.AddLast(q);
                        }
                    }
                }

                var tp = pp;
                pp = ppOther;
                ppOther = tp;
                var tb = bb;
                bb = bbOther;
                bbOther = tb;
            }

            return accept;
        }

        public static Automaton Union(IList<Automaton> automatons)
        {
            var ids = new HashSet<int>();
            foreach (var a in automatons)
            {
                _ = ids.Add(RuntimeHelpers.GetHashCode(a));
            }

            var hasAliases = ids.Count != automatons.Count;
            var s = new State();
            foreach (var b in automatons)
            {
                if (b.IsEmpty)
                {
                    continue;
                }

                var bb = b;
                bb = hasAliases ? bb.CloneExpanded() : bb.CloneExpandedIfRequired();
                s.AddEpsilon(bb.Initial);
            }

            var automaton = new Automaton
            {
                Initial = s,
                IsDeterministic = false,
            };
            automaton.ClearHashCode();
            automaton.CheckMinimizeAlways();
            return automaton;
        }
    }
}