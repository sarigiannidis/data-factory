// --------------------------------------------------------------------------------
// <copyright file="ValueFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;
    using System.Diagnostics;

    public abstract class ValueFactory<TValue, TConfiguration> : IValueFactory<TValue, TConfiguration>
        where TConfiguration : IValueFactoryConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TConfiguration _Configuration;

        public virtual TConfiguration Configuration
        {
            get => _Configuration;

            set
            {
                if (Equals(value, _Configuration))
                {
                    return;
                }

                _Configuration = value;
                OnConfigurationChanged();
            }
        }

        IValueFactoryConfiguration IValueFactory.Configuration
        {
            get => Configuration;
            set => Configuration = (TConfiguration)value;
        }

        public abstract bool IsRandom { get; }

        public event EventHandler ConfigurationChanged;

        public abstract TValue CreateValue();

        object IValueFactory.CreateValue() => CreateValue();

        private void OnConfigurationChanged() => ConfigurationChanged?.Invoke(this, EventArgs.Empty);
    }
}