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
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal class StatePair
        : IEquatable<StatePair>
    {
        public State FirstState { get; set; }

        public State S { get; set; }

        public State SecondState { get; set; }

        public StatePair(State s, State s1, State s2)
        {
            S = s;
            FirstState = s1;
            SecondState = s2;
        }

        public StatePair(State s1, State s2)
            : this(null, s1, s2)
        {
        }

        public static bool operator !=(StatePair left, StatePair right) => !Equals(left, right);

        public static bool operator ==(StatePair left, StatePair right) => Equals(left, right);

        public bool Equals(StatePair other) => !(other is null)
                && (ReferenceEquals(this, other)
                || (Equals(other.FirstState, FirstState)
                && Equals(other.SecondState, SecondState)));

        public override bool Equals(object obj) => !(obj is null) && (ReferenceEquals(this, obj) || (obj.GetType() == typeof(StatePair) && Equals((StatePair)obj)));

        public override int GetHashCode() => HashCode.Combine(FirstState, SecondState);
    }
}