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
    using System.Threading;

    [ExcludeFromCodeCoverage]
    internal class State
        : IEquatable<State>,
        IComparable<State>,
        IComparable
    {
        private static int _NextId;

        public bool Accept { get; set; }

        public int Id { get; } = Interlocked.Increment(ref _NextId);

        public int Number { get; set; }

        public IList<Transition> Transitions { get; set; } = new List<Transition>();

        public static bool operator !=(State left, State right) => !Equals(left, right);

        public static bool operator ==(State left, State right) => Equals(left, right);

        public void AddTransition(Transition t) => Transitions.Add(t);

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (obj.GetType() != typeof(State))
            {
                throw new ArgumentException("Object is not a State");
            }

            return CompareTo((State)obj);
        }

        public int CompareTo(State other) => other.Id - Id;

        public override bool Equals(object obj) => !(obj is null)
            && (ReferenceEquals(this, obj)
                || (obj.GetType() == typeof(State) && Equals((State)obj)));

        public bool Equals(State other) => !(other is null)
                && (ReferenceEquals(this, other)
                || (other.Id == Id
                && other.Accept.Equals(Accept)
                && other.Number == Number));

        public override int GetHashCode() => HashCode.Combine(Id, Accept, Number);

        public IList<Transition> GetSortedTransitions(bool toFirst)
        {
            var e = Transitions.ToArray();
            Array.Sort(e, new TransitionComparer(toFirst));
            return e.ToList();
        }

        public State Step(char c) => (from t in Transitions where t.Min <= c && c <= t.Max select t.To).FirstOrDefault();

        public void Step(char c, List<State> dest) => dest.AddRange(from t in Transitions where t.Min <= c && c <= t.Max select t.To);

        public override string ToString()
        {
            var sb = new StringBuilder();
            _ = sb.Append("state ").Append(Number);
            _ = sb.Append(Accept ? " [accept]" : " [reject]");
            _ = sb.Append(":\n");
            foreach (var t in Transitions)
            {
                _ = sb.Append("  ").Append(t.ToString()).Append("\n");
            }

            return sb.ToString();
        }

        internal void AddEpsilon(State to)
        {
            if (to.Accept)
            {
                Accept = true;
            }

            foreach (var t in to.Transitions)
            {
                Transitions.Add(t);
            }
        }

        internal void ResetTransitions() => Transitions = new List<Transition>();
    }
}