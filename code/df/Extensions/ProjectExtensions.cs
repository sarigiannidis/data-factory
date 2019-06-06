// --------------------------------------------------------------------------------
// <copyright file="ProjectExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io
{
    using Df.Extensibility;
    using Df.Io.Prescriptive;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class ProjectExtensions
    {
        public static ValueFactoryPrescription CreateValueFactoryPrescription(this Project project, IValueFactoryInfo valueFactoryInfo, Func<IValueFactoryInfo, IValueFactoryConfiguration> configurator)
        {
            var name = GenerateUniqueName(project.Prescriptor.ValueFactoryPrescriptions.Select(_ => _.Name), valueFactoryInfo.Name);
            return new ValueFactoryPrescription(name, valueFactoryInfo.Name, configurator(valueFactoryInfo));
        }

        private static string GenerateUniqueName(IEnumerable<string> items, string name) => Sequence(name)
            .Except(items)
            .First();

        private static IEnumerable<string> Sequence(string name)
        {
            var index = 0;
            while (true)
            {
                yield return "{0}{1}".FormatInvariant(name, index++);
            }
        }
    }
}