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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    [ExcludeFromCodeCoverage]
    internal static partial class MinimizationOperations
    {
        public static void Minimize(Automaton a)
        {
            if (!a.IsSingleton)
            {
                switch (Automaton.Minimization)
                {
                    case Automaton.MinimizeHuffman:
                        MinimizeHuffman(a);
                        break;

                    case Automaton.MinimizeBrzozowski:
                        MinimizeBrzozowski(a);
                        break;

                    default:
                        MinimizeHopcroft(a);
                        break;
                }
            }

            a.RecomputeHashCode();
        }

        public static void MinimizeBrzozowski(Automaton a)
        {
            if (a.IsSingleton)
            {
                return;
            }

            BasicOperations.Determinize(a, Reverse(a).ToList());
            BasicOperations.Determinize(a, Reverse(a).ToList());
        }

        public static void MinimizeHopcroft(Automaton a)
        {
            a.Determinize();
            var tr = a.Initial.Transitions;
            if (tr.Count == 1)
            {
                var t = tr[0];
                if (t.To == a.Initial && t.Min == char.MinValue && t.Max == char.MaxValue)
                {
                    return;
                }
            }

            a.Totalize();

            var ss = a.GetStates();
            var states = new State[ss.Count];
            var number = 0;
            foreach (var q in ss)
            {
                states[number] = q;
                q.Number = number++;
            }

            var sigma = a.GetStartPoints();

            var v = Enumerable.Repeat<LinkedList<State>>(default, sigma.Length);
            var reverse = new LinkedList<State>[states.Length][];
            for (var i = 0; i < states.Length; i++)
            {
                reverse[i] = v.ToArray();
            }

            var active1 = new StateList[states.Length, sigma.Length];
            var active2 = new StateListNode[states.Length, sigma.Length];
            var block = new int[states.Length];
            var partition = Enumerable.Repeat<LinkedList<State>>(default, states.Length).ToArray();
            var pending1 = new LinkedList<(int, int)>();
            var pending2 = new bool[sigma.Length, states.Length];
            var refine1 = new List<int>();
            var refine2 = new bool[states.Length];
            var reverseNonEmpty = new bool[states.Length, sigma.Length];
            var split1 = new List<State>();
            var split2 = new bool[states.Length];
            var splitBlock = Enumerable.Repeat<List<State>>(default, states.Length).ToArray();
            for (var q = 0; q < states.Length; q++)
            {
                splitBlock[q] = new List<State>();
                partition[q] = new LinkedList<State>();
                for (var x = 0; x < sigma.Length; x++)
                {
                    reverse[q][x] = new LinkedList<State>();
                    active1[q, x] = new StateList();
                }
            }

            foreach (var qq in states)
            {
                var j = qq.Accept ? 0 : 1;
                _ = partition[j].AddLast(qq);
                block[qq.Number] = j;
                for (var x = 0; x < sigma.Length; x++)
                {
                    var y = sigma[x];
                    var p = qq.Step(y);
                    _ = reverse[p.Number][x].AddLast(qq);
                    reverseNonEmpty[p.Number, x] = true;
                }
            }

            for (var j = 0; j <= 1; j++)
            {
                for (var x = 0; x < sigma.Length; x++)
                {
                    foreach (var qq in partition[j])
                    {
                        if (reverseNonEmpty[qq.Number, x])
                        {
                            active2[qq.Number, x] = active1[j, x].Add(qq);
                        }
                    }
                }
            }

            for (var x = 0; x < sigma.Length; x++)
            {
                var j = active1[0, x].Size <= active1[1, x].Size ? 0 : 1;
                _ = pending1.AddLast((j, x));
                pending2[x, j] = true;
            }

            var k = 2;
            while (pending1.Count > 0)
            {
                var ip = pending1.RemoveAndReturnFirst();
                pending2[ip.Item2, ip.Item1] = false;

                for (var m = active1[ip.Item1, ip.Item2].First; m != null; m = m.Next)
                {
                    foreach (var s in reverse[m.State.Number][ip.Item2])
                    {
                        if (!split2[s.Number])
                        {
                            split2[s.Number] = true;
                            split1.Add(s);
                            var j = block[s.Number];
                            splitBlock[j].Add(s);
                            if (!refine2[j])
                            {
                                refine2[j] = true;
                                refine1.Add(j);
                            }
                        }
                    }
                }

                foreach (var j in refine1)
                {
                    if (splitBlock[j].Count < partition[j].Count)
                    {
                        var b1 = partition[j];
                        var b2 = partition[k];
                        foreach (var s in splitBlock[j])
                        {
                            _ = b1.Remove(s);
                            _ = b2.AddLast(s);
                            block[s.Number] = k;
                            for (var c = 0; c < sigma.Length; c++)
                            {
                                var sn = active2[s.Number, c];
                                if (sn != null && sn.StateList == active1[j, c])
                                {
                                    sn.Remove();
                                    active2[s.Number, c] = active1[k, c].Add(s);
                                }
                            }
                        }

                        for (var c = 0; c < sigma.Length; c++)
                        {
                            var aj = active1[j, c].Size;
                            var ak = active1[k, c].Size;
                            if (!pending2[c, j] && aj > 0 && aj <= ak)
                            {
                                pending2[c, j] = true;
                                _ = pending1.AddLast((j, c));
                            }
                            else
                            {
                                pending2[c, k] = true;
                                _ = pending1.AddLast((k, c));
                            }
                        }

                        k++;
                    }

                    foreach (var s in splitBlock[j])
                    {
                        split2[s.Number] = false;
                    }

                    refine2[j] = false;
                    splitBlock[j].Clear();
                }

                split1.Clear();
                refine1.Clear();
            }

            var newstates = new State[k];
            for (var n = 0; n < newstates.Length; n++)
            {
                var s = new State();
                newstates[n] = s;
                foreach (var q in partition[n])
                {
                    if (q == a.Initial)
                    {
                        a.Initial = s;
                    }

                    s.Accept = q.Accept;
                    s.Number = q.Number;
                    q.Number = n;
                }
            }

            foreach (var s in newstates)
            {
                s.Accept = states[s.Number].Accept;
                foreach (var t in states[s.Number].Transitions)
                {
                    s.Transitions.Add(new Transition(t.Min, t.Max, newstates[t.To.Number]));
                }
            }

            a.RemoveDeadTransitions();
        }

        public static void MinimizeHuffman(Automaton a)
        {
            a.Determinize();
            a.Totalize();
            var ss = a.GetStates();
            var transitions = new Transition[ss.Count][];
            var states = ss.ToArray();
            var mark = new bool[states.Length, states.Length];
            var triggers = new List<HashSet<(int, int)>>[states.Length];
            var v = Enumerable.Repeat<HashSet<(int, int)>>(default, states.Length);
            for (var i = 0; i < states.Length; i++)
            {
                triggers[i] = v.ToList();
            }

            for (var n1 = 0; n1 < states.Length; n1++)
            {
                states[n1].Number = n1;
                transitions[n1] = states[n1].GetSortedTransitions(false).ToArray();
                for (var n2 = n1 + 1; n2 < states.Length; n2++)
                {
                    if (states[n1].Accept != states[n2].Accept)
                    {
                        mark[n1, n2] = true;
                    }
                }
            }

            for (var n1 = 0; n1 < states.Length; n1++)
            {
                for (var n2 = n1 + 1; n2 < states.Length; n2++)
                {
                    if (mark[n1, n2])
                    {
                        continue;
                    }

                    if (StatesAgree(transitions, mark, n1, n2))
                    {
                        AddTriggers(transitions, triggers, n1, n2);
                    }
                    else
                    {
                        MarkPair(mark, triggers, n1, n2);
                    }
                }
            }

            var numclasses = 0;
            foreach (var t in states)
            {
                t.Number = -1;
            }

            for (var n1 = 0; n1 < states.Length; n1++)
            {
                if (states[n1].Number != -1)
                {
                    continue;
                }

                states[n1].Number = numclasses;
                for (var n2 = n1 + 1; n2 < states.Length; n2++)
                {
                    if (!mark[n1, n2])
                    {
                        states[n2].Number = numclasses;
                    }
                }

                numclasses++;
            }

            var newstates = new State[numclasses];
            for (var n = 0; n < numclasses; n++)
            {
                newstates[n] = new State();
            }

            for (var n = 0; n < states.Length; n++)
            {
                newstates[states[n].Number].Number = n;
                if (states[n] == a.Initial)
                {
                    a.Initial = newstates[states[n].Number];
                }
            }

            for (var n = 0; n < numclasses; n++)
            {
                var s = newstates[n];
                s.Accept = states[s.Number].Accept;
                foreach (var t in states[s.Number].Transitions)
                {
                    s.Transitions.Add(new Transition(t.Min, t.Max, newstates[t.To.Number]));
                }
            }

            a.RemoveDeadTransitions();
        }

        private static void AddTriggers(Transition[][] transitions, List<HashSet<(int, int)>>[] triggers, int n1, int n2)
        {
            var t1 = transitions[n1];
            var t2 = transitions[n2];
            for (int k1 = 0, k2 = 0; k1 < t1.Length && k2 < t2.Length;)
            {
                if (t1[k1].Max < t2[k2].Min)
                {
                    k1++;
                }
                else if (t2[k2].Max < t1[k1].Min)
                {
                    k2++;
                }
                else
                {
                    if (t1[k1].To != t2[k2].To)
                    {
                        var m1 = t1[k1].To.Number;
                        var m2 = t2[k2].To.Number;
                        if (m1 > m2)
                        {
                            var t = m1;
                            m1 = m2;
                            m2 = t;
                        }

                        if (triggers[m1][m2] == null)
                        {
                            triggers[m1].Insert(m2, new HashSet<(int, int)>());
                        }

                        _ = triggers[m1][m2].Add((n1, n2));
                    }

                    if (t1[k1].Max < t2[k2].Max)
                    {
                        k1++;
                    }
                    else
                    {
                        k2++;
                    }
                }
            }
        }

        private static void MarkPair(bool[,] mark, List<HashSet<(int, int)>>[] triggers, int n1, int n2)
        {
            mark[n1, n2] = true;
            if (triggers[n1][n2] == null)
            {
                return;
            }

            foreach (var p in triggers[n1][n2])
            {
                var m1 = p.Item1;
                var m2 = p.Item2;
                if (m1 > m2)
                {
                    var t = m1;
                    m1 = m2;
                    m2 = t;
                }

                if (!mark[m1, m2])
                {
                    MarkPair(mark, triggers, m1, m2);
                }
            }
        }

        private static HashSet<State> Reverse(Automaton a)
        {
            var m = new Dictionary<State, HashSet<Transition>>();
            var states = a.GetStates();
            var accept = a.GetAcceptStates();
            foreach (var r in states)
            {
                m.Add(r, new HashSet<Transition>());
                r.Accept = false;
            }

            foreach (var r in states)
            {
                foreach (var t in r.Transitions)
                {
                    _ = m[t.To].Add(new Transition(t.Min, t.Max, r));
                }
            }

            foreach (var r in states)
            {
                r.ResetTransitions();
                r.Transitions = m[r].ToList();
            }

            a.Initial.Accept = true;
            a.Initial = new State();
            foreach (var r in accept)
            {
                a.Initial.AddEpsilon(r);
            }

            a.IsDeterministic = false;
            return accept;
        }

        private static bool StatesAgree(Transition[][] transitions, bool[,] mark, int n1, int n2)
        {
            var t1 = transitions[n1];
            var t2 = transitions[n2];
            for (int k1 = 0, k2 = 0; k1 < t1.Length && k2 < t2.Length;)
            {
                if (t1[k1].Max < t2[k2].Min)
                {
                    k1++;
                }
                else if (t2[k2].Max < t1[k1].Min)
                {
                    k2++;
                }
                else
                {
                    var m1 = t1[k1].To.Number;
                    var m2 = t2[k2].To.Number;
                    if (m1 > m2)
                    {
                        var t = m1;
                        m1 = m2;
                        m2 = t;
                    }

                    if (mark[m1, m2])
                    {
                        return false;
                    }

                    if (t1[k1].Max < t2[k2].Max)
                    {
                        k1++;
                    }
                    else
                    {
                        k2++;
                    }
                }
            }

            return true;
        }
    }
}