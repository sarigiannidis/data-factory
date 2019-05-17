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
    using System.Linq;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal static class BasicAutomata
    {
        private static Automaton WhitespaceAutomaton { get; } = Automaton.Minimize(Automaton.MakeCharSet(" \t\n\r").Repeat());

        /// <summary>
        /// Returns a new (deterministic) automaton that accepts any single character.
        /// </summary>
        /// <returns>
        /// A new (deterministic) automaton that accepts any single character.
        /// </returns>
        public static Automaton MakeAnyChar() => MakeCharRange(char.MinValue, char.MaxValue);

        /// <summary>
        /// Returns a new (deterministic) automaton that accepts all strings.
        /// </summary>
        /// <returns>
        /// A new (deterministic) automaton that accepts all strings.
        /// </returns>
        public static Automaton MakeAnyString()
        {
            var state = new State
            {
                Accept = true,
            };
            state.Transitions.Add(new Transition(char.MinValue, char.MaxValue, state));

            var a = new Automaton
            {
                Initial = state,
                IsDeterministic = true,
            };
            return a;
        }

        /// <summary>
        /// Returns a new (deterministic) automaton that accepts a single character of the given value.
        /// </summary>
        /// <param name="c">
        /// The c.
        /// </param>
        /// <returns>
        /// A new (deterministic) automaton that accepts a single character of the given value.
        /// </returns>
        public static Automaton MakeChar(char c)
        {
            var a = new Automaton
            {
                Singleton = c.ToString(),
                IsDeterministic = true,
            };
            return a;
        }

        /// <summary>
        /// Returns a new (deterministic) automaton that accepts a single char whose value is in the
        /// given interval (including both end points).
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// A new (deterministic) automaton that accepts a single char whose value is in the given
        /// interval (including both end points).
        /// </returns>
        public static Automaton MakeCharRange(char min, char max)
        {
            if (min == max)
            {
                return MakeChar(min);
            }

            var a = new Automaton();
            var s1 = new State();
            var s2 = new State();
            a.Initial = s1;
            s2.Accept = true;
            if (min <= max)
            {
                s1.Transitions.Add(new Transition(min, max, s2));
            }

            a.IsDeterministic = true;
            return a;
        }

        /// <summary>
        /// Returns a new (deterministic) automaton that accepts a single character in the given set.
        /// </summary>
        /// <param name="set">
        /// The set.
        /// </param>
        public static Automaton MakeCharSet(string set)
        {
            if (set.Length == 1)
            {
                return MakeChar(set[0]);
            }

            var a = new Automaton();
            var s1 = new State();
            var s2 = new State();

            a.Initial = s1;
            s2.Accept = true;

            foreach (var t in set)
            {
                s1.Transitions.Add(new Transition(t, s2));
            }

            a.IsDeterministic = true;
            a.Reduce();

            return a;
        }

        /// <summary>
        /// Constructs automaton that accept strings representing the given decimal number.
        /// Surrounding whitespace is permitted.
        /// </summary>
        /// <param name="value">
        /// The value string representation of decimal number.
        /// </param>
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
            return Automaton.Minimize(
                WhitespaceAutomaton.Concatenate(
                    s.Concatenate(Automaton.MakeChar('0').Repeat())
                        .Concatenate(Automaton.MakeString(sb1.ToString()))
                        .Concatenate(d))
                    .Concatenate(WhitespaceAutomaton));
        }

        /// <summary>
        /// Returns a new (deterministic) automaton with the empty language.
        /// </summary>
        /// <returns>
        /// A new (deterministic) automaton with the empty language.
        /// </returns>
        public static Automaton MakeEmpty()
        {
            var a = new Automaton();
            var s = new State();
            a.Initial = s;
            a.IsDeterministic = true;
            return a;
        }

        /// <summary>
        /// Returns a new (deterministic) automaton that accepts only the empty string.
        /// </summary>
        /// <returns>
        /// A new (deterministic) automaton that accepts only the empty string.
        /// </returns>
        public static Automaton MakeEmptyString()
        {
            var a = new Automaton
            {
                Singleton = string.Empty,
                IsDeterministic = true,
            };
            return a;
        }

        /// <summary>
        /// Constructs automaton that accept strings representing decimal numbers that can be written
        /// with at most the given number of digits in the fraction part. Surrounding whitespace is permitted.
        /// </summary>
        /// <param name="i">
        /// The i max number of necessary fraction digits.
        /// </param>
        public static Automaton MakeFractionDigits(int i) => Automaton.Minimize(
                    new RegExp("[ \t\n\r]*[-+]?[0-9]+(\\.[0-9]{0," + i + "}0*)?[ \t\n\r]*")
                        .ToAutomaton());

        /// <summary>
        /// Constructs automaton that accept strings representing the given integer. Surrounding
        /// whitespace is permitted.
        /// </summary>
        /// <param name="value">
        /// The value string representation of integer.
        /// </param>
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

        /// <summary>
        /// Returns a new automaton that accepts strings representing decimal non-negative integers
        /// in the given interval.
        /// </summary>
        /// <param name="min">
        /// The minimum value of interval.
        /// </param>
        /// <param name="max">
        /// The maximum value of inverval (both end points are included in the interval).
        /// </param>
        /// <param name="digits">
        /// If f &gt;0, use fixed number of digits (strings must be prefixed by 0's to obtain the
        /// right length) otherwise, the number of digits is not fixed.
        /// </param>
        /// <returns>
        /// A new automaton that accepts strings representing decimal non-negative integers in the
        /// given interval.
        /// </returns>
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

        /// <summary>
        /// Constructs automaton that accept strings representing nonnegative integer that are not
        /// larger than the given value.
        /// </summary>
        /// <param name="n">
        /// The n string representation of maximum value.
        /// </param>
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

        /// <summary>
        /// Constructs automaton that accept strings representing nonnegative integers that are not
        /// less that the given value.
        /// </summary>
        /// <param name="n">
        /// The n string representation of minimum value.
        /// </param>
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

        /// <summary>
        /// Returns a new (deterministic) automaton that accepts the single given string.
        /// </summary>
        /// <param name="s">
        /// The string.
        /// </param>
        /// <returns>
        /// A new (deterministic) automaton that accepts the single given string.
        /// </returns>
        public static Automaton MakeString(string s)
        {
            var a = new Automaton
            {
                Singleton = s,
                IsDeterministic = true,
            };
            return a;
        }

        /// <summary>
        /// Constructs deterministic automaton that matches strings that contain the given substring.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
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
                    if (!done.Contains(d) && s.Substring(0, j - 1).Equals(s[i - j + 1..i]))
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

        /// <summary>
        /// Returns a new (deterministic and minimal) automaton that accepts the union of the given
        /// set of strings. The input character sequences are internally sorted in-place, so the
        /// input array is modified. @see StringUnionOperations.
        /// </summary>
        /// <param name="strings">
        /// The strings.
        /// </param>
        public static Automaton MakeStringUnion(params char[][] strings)
        {
            if (strings.Length == 0)
            {
                return MakeEmpty();
            }

            Array.Sort(strings, StringUnionOperations.LexicographicOrderComparer);
            var a = new Automaton
            {
                Initial = StringUnionOperations.Build(strings),
                IsDeterministic = true,
            };
            a.Reduce();
            a.RecomputeHashCode();
            return a;
        }

        /// <summary>
        /// Constructs automaton that accept strings representing decimal numbers that can be written
        /// with at most the given number of digits. Surrounding whitespace is permitted.
        /// </summary>
        /// <param name="i">
        /// The i max number of necessary digits.
        /// </param>
        public static Automaton MakeTotalDigits(int i) => Automaton.Minimize(
                    new RegExp("[ \t\n\r]*[-+]?0*([0-9]{0," + i + "}|((([0-9]\\.*){0," + i + "})&@\\.@)0*)[ \t\n\r]*")
                       .ToAutomaton());

        /// <summary>
        /// Constructs sub-automaton corresponding to decimal numbers of length x.Substring(n).Length.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="n">
        /// The n.
        /// </param>
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

        /// <summary>
        /// Constructs sub-automaton corresponding to decimal numbers of value at least
        /// x.Substring(n) and length x.Substring(n).Length.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="n">
        /// The n.
        /// </param>
        /// <param name="initials">
        /// The initials.
        /// </param>
        /// <param name="zeros">
        /// if set to <c>true</c> [zeros].
        /// </param>
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

        /// <summary>
        /// Constructs sub-automaton corresponding to decimal numbers of value at most x.Substring(n)
        /// and length x.Substring(n).Length.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="n">
        /// The n.
        /// </param>
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

        /// <summary>
        /// Constructs sub-automaton corresponding to decimal numbers of value between x.Substring(n)
        /// and y.Substring(n) and of length x.Substring(n).Length (which must be equal to y.Substring(n).Length).
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <param name="n">
        /// The n.
        /// </param>
        /// <param name="initials">
        /// The initials.
        /// </param>
        /// <param name="zeros">
        /// if set to <c>true</c> [zeros].
        /// </param>
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
                    // cx < cy
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