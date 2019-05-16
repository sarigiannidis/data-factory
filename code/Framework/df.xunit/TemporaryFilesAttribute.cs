// --------------------------------------------------------------------------------
// <copyright file="TemporaryFilesAttribute.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Xunit
{
    using System;
    using System.Reflection;
    using Xunit.Sdk;
    using static Constants;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class TemporaryFilesAttribute
        : BeforeAfterTestAttribute
    {
        public string Extension { get; set; } = DEFAULTEXTENSION;

        public string Prefix { get; set; }

        public override void After(MethodInfo methodUnderTest) =>
            TemporaryFileContext.GetContext(methodUnderTest)?.Dispose();

        public override void Before(MethodInfo methodUnderTest)
        {
            if (string.IsNullOrWhiteSpace(Prefix))
            {
                Prefix = methodUnderTest.Name;
            }

            _ = TemporaryFileContext.AddContext(methodUnderTest, Prefix, Extension);
        }
    }
}