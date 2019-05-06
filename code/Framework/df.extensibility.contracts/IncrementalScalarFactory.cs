// --------------------------------------------------------------------------------
// <copyright file="IncrementalScalarFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public abstract class IncrementalScalarFactory<TValue>
        : ValueFactory<TValue, IScalarFactoryConfiguration<TValue>>
        where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TValue _Next = default;

        public override bool IsRandom => false;

        protected IncrementalScalarFactory() =>
            ConfigurationChanged += Reset;

        public override TValue CreateValue()
        {
            var result = _Next;
            _Next = (TValue)((dynamic)_Next + Configuration.Increment);
            if (Comparer<TValue>.Default.Compare(_Next, Configuration.MaxValue) > 0)
            {
                _Next = Configuration.MinValue;
            }

            return result;
        }

        private void Reset(object sender, EventArgs e) =>
            _Next = Configuration.MinValue;
    }
}