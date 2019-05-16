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

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal sealed class TransitionComparer : IComparer<Transition>
    {
        private readonly bool _ToFirst;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransitionComparer"/> class.
        /// </summary>
        /// <param name="toFirst">
        /// if set to <c>true</c> [to first].
        /// </param>
        public TransitionComparer(bool toFirst) => _ToFirst = toFirst;

        /// <summary>
        /// Compares by (min, reverse max, to) or (to, min, reverse max).
        /// </summary>
        /// <param name="x">
        /// The first Transition.
        /// </param>
        /// <param name="y">
        /// The second Transition.
        /// </param>
        public int Compare(Transition x, Transition y)
        {
            if (_ToFirst && x.To != y.To)
            {
                if (x.To == null)
                {
                    return -1;
                }
                else if (y.To == null)
                {
                    return 1;
                }
                else if (x.To.Number < y.To.Number)
                {
                    return -1;
                }
                else if (x.To.Number > y.To.Number)
                {
                    return 1;
                }
            }
            else if (x.Min < y.Min)
            {
                return -1;
            }
            else if (x.Min > y.Min)
            {
                return 1;
            }
            else if (x.Max > y.Max)
            {
                return -1;
            }
            else if (x.Max < y.Max)
            {
                return 1;
            }
            else if (!_ToFirst && x.To != y.To)
            {
                if (x.To == null)
                {
                    return -1;
                }
                else if (y.To == null)
                {
                    return 1;
                }
                else if (x.To.Number < y.To.Number)
                {
                    return -1;
                }
                else if (x.To.Number > y.To.Number)
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}