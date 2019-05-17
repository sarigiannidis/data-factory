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
    using System.Text;

    /// <summary>
    ///  <tt>Automaton</tt> transition.
    ///  <p>
    ///    A transition, which belongs to a source state, consists of a Unicode character interval
    ///    and a destination state.
    ///  </p>
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class Transition : IEquatable<Transition>
    {
        /// <summary>
        /// Gets the maximum of this transition interval.
        /// </summary>
        public char Max { get; }

        /// <summary>
        /// Gets the minimum of this transition interval.
        /// </summary>
        public char Min { get; }

        /// <summary>
        /// Gets the destination of this transition.
        /// </summary>
        public State To { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transition"/> class. (Constructs a new
        /// singleton interval transition).
        /// </summary>
        /// <param name="c">
        /// The transition character.
        /// </param>
        /// <param name="to">
        /// The destination state.
        /// </param>
        public Transition(char c, State to)
        {
            Min = Max = c;
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transition"/> class. (Both end points are
        /// included in the interval).
        /// </summary>
        /// <param name="min">
        /// The transition interval minimum.
        /// </param>
        /// <param name="max">
        /// The transition interval maximum.
        /// </param>
        /// <param name="to">
        /// The destination state.
        /// </param>
        public Transition(char min, char max, State to)
        {
            if (max < min)
            {
                var t = max;
                max = min;
                min = t;
            }

            Min = min;
            Max = max;
            To = to;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Transition left, Transition right) => !Equals(left, right);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Transition left, Transition right) => Equals(left, right);

        /// <inheritdoc/>
        ///
        public override bool Equals(object obj) =>
            !(obj is null)
            && (ReferenceEquals(this, obj)
                || (obj.GetType() == typeof(Transition) && Equals((Transition)obj)));

        /// <inheritdoc/>
        ///
        public bool Equals(Transition other) =>
            !(other is null)
                && (ReferenceEquals(this, other)
                || (other.Min == Min
                   && other.Max == Max
                   && Equals(other.To, To)));

        /// <inheritdoc/>
        ///
        public override int GetHashCode() =>
            HashCode.Combine(Min, Max, To);

        /// <inheritdoc/>
        ///
        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendCharString(sb, Min);
            if (Min != Max)
            {
                _ = sb.Append("-");
                AppendCharString(sb, Max);
            }

            _ = sb.Append(" -> ").Append(To.Number);
            return sb.ToString();
        }

        private static void AppendCharString(StringBuilder sb, char c)
        {
            if (c >= 0x21 && c <= 0x7e && c != '\\' && c != '"')
            {
                _ = sb.Append(c);
            }
            else
            {
                _ = sb.Append("\\u");
                var s = ((int)c).ToString("x");
                _ = c < 0x10
                    ? sb.Append("000").Append(s)
                    : c < 0x100 ? sb.Append("00").Append(s) : c < 0x1000 ? sb.Append("0").Append(s) : sb.Append(s);
            }
        }
    }
}