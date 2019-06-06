// --------------------------------------------------------------------------------
// <copyright file="SerialTestCaseOrderer.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Xunit
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit.Abstractions;
    using Xunit.Sdk;
    using static Constants;

    /// <summary>
    /// To order the tests in a test class:
    /// Apply the attribute [TestCaseOrderer("Xunit.SerialTestCaseOrderer", "df.xunit")] to the class.
    /// Apply the attribute [OrderTrait(_)] to the test methods.
    /// </summary>
    public sealed class SerialTestCaseOrderer
        : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase => testCases.OrderBy(KeySelector);

        private int KeySelector<TTestCase>(TTestCase testCase)
            where TTestCase : ITestCase => testCase.Traits.TryGetValue(KEY_ORDER, out var orderValues)
                && orderValues.Count == 1
                && int.TryParse(orderValues[0], out var value)
                    ? value
                    : int.MaxValue;
    }
}