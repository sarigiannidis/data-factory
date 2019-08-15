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
    using System.Text;

    [ExcludeFromCodeCoverage]
    internal static class BasicAutomata
    {
        private static Automaton WhitespaceAutomaton { get; } = Automaton.Minimize(Automaton.MakeCharSet(" \t\n\r").Repeat());

        public static Automaton MakeAnyChar() => MakeCharRange(char.MinValue, char.MaxValue);

        public static Automaton MakeAnyString()
        {
            var state = new State
            {
                Accept = true,
            };
            state.Transitions.Add(new Transition(char.MinValue, char.MaxValue, state));

            return new Automaton
            {
                Initial = state,
                IsDeterministic = true,
            };
        }

        public static Automaton MakeChar(char c) => new Automaton
        {
            Singleton = c.ToString(),
            IsDeterministic = true,
        };

        public static Automaton MakeCharRange(char min, char max)
        {
            if (min == max)
            {
                return MakeChar(min);
            }

            var s2 = new State
            {
                Accept = true,
            };
            var s1 = new State();
            var a = new Automaton
            {
                Initial = s1,
            };
            if (min <= max)
            {
                s1.Transitions.Add(new Transition(min, max, s2));
            }

            a.IsDeterministic = true;
            return a;
        }

        public static Automaton MakeCharSet(string set)
        {
            if (set.Length == 1)
            {
                return MakeChar(set[0]);
            }

            var s1 = new State();
            var a = new Automaton
            {
                Initial = s1,
            };
            var s2 = new State
            {
                Accept = true,
            };

            foreach (var t in set)
            {
                s1.Transitions.Add(new Transition(t, s2));
            }

            a.IsDeterministic = true;
            a.Reduce();

            return a;
        }

        public static Automaton MakeDecimalValue(string value)
        {
            var minus = false;
            var i = 0;
            while (i < value.Length)
            {
                var c = value[i];
                if (c == '-')
                {
                    minus = true;
                }

                if ((c >= '1' && c <= '9') || c == '.')
                {
                    break;
                }

                i++;
            }

            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var p = value.IndexOf('.', i);
            if (p == -1)
            {
                _ = sb1.Append(value, i, value.Length - i);
            }
            else
            {
                _ = sb1.Append(value, i, p - i);
                i = value.Length - 1;
                while (i > p)
                {
                    var c = value[i];
                    if (c >= '1' && c <= '9')
                    {
                        break;
                    }

                    i--;
                }

                _ = sb2.Append(value, p + 1, i + 1 - (p + 1));
            }

            if (sb1.Length == 0)
            {
                _ = sb1.Append("0");
            }

            var s = minus ? Automaton.MakeChar('-') : Automaton.MakeChar('+').Optional();
            var d = sb2.Length == 0
                ? Automaton.MakeChar('.').Concatenate(Automaton.MakeChar('0').Repeat(1)).Optional()
                : Automaton.MakeChar('.')
                    .Concatenate(Automaton.MakeString(sb2.ToString()))
                    .Concatenate(Automaton.MakeChar('0')
                                     .Repeat());
            return Automaton.Minimize(WhitespaceAutomaton
                .Concatenate(s.Concatenate(Automaton.MakeChar('0').Repeat()).Concatenate(Automaton.MakeString(sb1.ToString())).Concatenate(d))
                .Concatenate(WhitespaceAutomaton));
        }

        public static Automaton MakeEmpty() => new Automaton
        {
            Initial = new State(),
            IsDeterministic = true,
        };

        public static Automaton MakeEmptyString() => new Automaton
        {
            Singleton = string.Empty,
            IsDeterministic = true,
        };

        public static Automaton MakeFractionDigits(int i) => Automaton.Minimize(
                    new RegExp("[ \t\n\r]*[-+]?[0-9]+(\\.[0-9]{0," + i + "}0*)?[ \t\n\r]*")
                        .ToAutomaton());

        public static Automaton MakeIntegerValue(string value)
        {
            var minus = false;
            var i = 0;
            while (i < value.Length)
            {
                var c = value[i];
                if (c == '-')
                {
                    minus = true;
                }

                if (c >= '1' && c <= '9')
                {
                    break;
                }

                i++;
            }

            var sb = new StringBuilder();
            _ = sb.Append(value, i, value.Length - i);
            if (sb.Length == 0)
            {
                _ = sb.Append("0");
            }

            var s = minus ? Automaton.MakeChar('-') : Automaton.MakeChar('+').Optional();
            return Automaton.Minimize(
                WhitespaceAutomaton.Concatenate(
                    s.Concatenate(Automaton.MakeChar('0').Repeat())
                        .Concatenate(Automaton.MakeString(sb.ToString())))
                    .Concatenate(WhitespaceAutomaton));
        }

        public static Automaton MakeInterval(int min, int max, int digits)
        {
            var a = new Automaton();
            var x = Convert.ToString(min);
            var y = Convert.ToString(max);
            if (min > max || (digits > 0 && y.Length > digits))
            {
                throw new ArgumentException();
            }

            var d = digits > 0 ? digits : y.Length;
            var sb1 = new StringBuilder();
            for (var i = x.Length; i < d; i++)
            {
                _ = sb1.Append('0');
            }

            _ = sb1.Append(x);
            x = sb1.ToString();
            var sb2 = new StringBuilder();
            for (var i = y.Length; i < d; i++)
            {
                _ = sb2.Append('0');
            }

            _ = sb2.Append(y);
            y = sb2.ToString();
            ICollection<State> initials = new List<State>();
            a.Initial = Between(x, y, 0, initials, digits <= 0);
            if (digits <= 0)
            {
                var pairs = (from p in initials
                             where a.Initial != p
                             select new StatePair(a.Initial, p)).ToList();
                a.AddEpsilons(pairs);
                a.Initial.AddTransition(new Transition('0', a.Initial));
                a.IsDeterministic = false;
            }
            else
            {
                a.IsDeterministic = true;
            }

            a.CheckMinimizeAlways();
            return a;
        }

        public static Automaton MakeMaxInteger(string n)
        {
            var i = 0;
            while (i < n.Length && n[i] == '0')
            {
                i++;
            }

            var sb = new StringBuilder();
            _ = sb.Append("0*(0|");
            if (i < n.Length)
            {
                _ = sb.Append("[0-9]{1,").Append(n.Length - i - 1).Append("}|");
            }

            MaxInteger(sb, n.Substring(i), 0);
            _ = sb.Append(")");
            return Automaton.Minimize(new RegExp(sb.ToString()).ToAutomaton());
        }

        public static Automaton MakeMinInteger(string n)
        {
            var i = 0;
            while (i + 1 < n.Length && n[i] == '0')
            {
                i++;
            }

            var sb = new StringBuilder();
            _ = sb.Append("0*");
            MinInteger(sb, n.Substring(i), 0);
            _ = sb.Append("[0-9]*");
            return Automaton.Minimize(new RegExp(sb.ToString()).ToAutomaton());
        }

        public static Automaton MakeString(string s) => new Automaton
        {
            Singleton = s,
            IsDeterministic = true,
        };

        public static Automaton MakeStringMatcher(string s)
        {
            var a = new Automaton();
            var states = new State[s.Length + 1];
            states[0] = a.Initial;
            for (var i = 0; i < s.Length; i++)
            {
                states[i + 1] = new State();
            }

            var f = states[s.Length];
            f.Accept = true;
            f.Transitions.Add(new Transition(char.MinValue, char.MaxValue, f));
            for (var i = 0; i < s.Length; i++)
            {
                var done = new HashSet<char?>();
                var c = s[i];
                states[i].Transitions.Add(new Transition(c, states[i + 1]));
                _ = done.Add(c);
                for (var j = i; j >= 1; j--)
                {
                    var d = s[j - 1];
                    if (!done.Contains(d) && s.Substring(0, j - 1).Equals(s.Substring(i - j + 1, i - (i - j + 1))))
                    {
                        states[i].Transitions.Add(new Transition(d, states[j]));
                        _ = done.Add(d);
                    }
                }

                var da = new char[done.Count];
                var h = 0;
                foreach (char w in done)
                {
                    da[h++] = w;
                }

                Array.Sort(da);
                int from = char.MinValue;
                var k = 0;
                while (from <= char.MaxValue)
                {
                    while (k < da.Length && da[k] == from)
                    {
                        k++;
                        from++;
                    }

                    if (from <= char.MaxValue)
                    {
                        int to = char.MaxValue;
                        if (k < da.Length)
                        {
                            to = da[k] - 1;
                            k++;
                        }

                        states[i].Transitions.Add(new Transition((char)from, (char)to, states[0]));
                        from = to + 2;
                    }
                }
            }

            a.IsDeterministic = true;
            return a;
        }

        public static Automaton MakeStringUnion(params char[][] strings)
        {
            if (strings.Length == 0)
            {
                return MakeEmpty();
            }

            Array.Sort(strings, new LexicographicComparer());
            var a = new Automaton
            {
                Initial = StringUnionOperations.Build(strings),
                IsDeterministic = true,
            };
            a.Reduce();
            a.RecomputeHashCode();
            return a;
        }

        public static Automaton MakeTotalDigits(int i) => Automaton.Minimize(
                    new RegExp("[ \t\n\r]*[-+]?0*([0-9]{0," + i + "}|((([0-9]\\.*){0," + i + "})&@\\.@)0*)[ \t\n\r]*")
                       .ToAutomaton());

        private static State AnyOfRightLength(string x, int n)
        {
            var s = new State();

            if (x.Length == n)
            {
                s.Accept = true;
            }
            else
            {
                s.AddTransition(new Transition('0', '9', AnyOfRightLength(x, n + 1)));
            }

            return s;
        }

        private static State AtLeast(string x, int n, ICollection<State> initials, bool zeros)
        {
            var s = new State();
            if (x.Length == n)
            {
                s.Accept = true;
            }
            else
            {
                if (zeros)
                {
                    initials.Add(s);
                }

                var c = x[n];
                s.AddTransition(new Transition(c, AtLeast(x, n + 1, initials, zeros && c == '0')));
                if (c < '9')
                {
                    s.AddTransition(new Transition((char)(c + 1), '9', AnyOfRightLength(x, n + 1)));
                }
            }

            return s;
        }

        private static State AtMost(string x, int n)
        {
            var s = new State();

            if (x.Length == n)
            {
                s.Accept = true;
            }
            else
            {
                var c = x[n];
                s.AddTransition(new Transition(c, AtMost(x, (char)n + 1)));
                if (c > '0')
                {
                    s.AddTransition(new Transition('0', (char)(c - 1), AnyOfRightLength(x, n + 1)));
                }
            }

            return s;
        }

        private static State Between(string x, string y, int n, ICollection<State> initials, bool zeros)
        {
            var s = new State();

            if (x.Length == n)
            {
                s.Accept = true;
            }
            else
            {
                if (zeros)
                {
                    initials.Add(s);
                }

                var cx = x[n];
                var cy = y[n];
                if (cx == cy)
                {
                    s.AddTransition(new Transition(cx, Between(x, y, n + 1, initials, zeros && cx == '0')));
                }
                else
                {
                    s.AddTransition(new Transition(cx, AtLeast(x, n + 1, initials, zeros && cx == '0')));
                    s.AddTransition(new Transition(cy, AtMost(y, n + 1)));
                    if (cx + 1 < cy)
                    {
                        s.AddTransition(new Transition((char)(cx + 1), (char)(cy - 1), AnyOfRightLength(x, n + 1)));
                    }
                }
            }

            return s;
        }

        private static void MaxInteger(StringBuilder sb, string n, int i)
        {
            _ = sb.Append('(');
            if (i < n.Length)
            {
                var c = n[i];
                if (c != '0')
                {
                    _ = sb.Append("[0-").Append((char)(c - 1)).Append("][0-9]{").Append(n.Length - i - 1).Append("}|");
                }

                _ = sb.Append(c);
                MaxInteger(sb, n, i + 1);
            }

            _ = sb.Append(')');
        }

        private static void MinInteger(StringBuilder sb, string n, int i)
        {
            _ = sb.Append('(');
            if (i < n.Length)
            {
                var c = n[i];
                if (c != '9')
                {
                    _ = sb.Append('[').Append((char)(c + 1)).Append("-9][0-9]{").Append(n.Length - i - 1).Append("}|");
                }

                _ = sb.Append(c);
                MinInteger(sb, n, i + 1);
            }

            _ = sb.Append(')');
        }
    }
}