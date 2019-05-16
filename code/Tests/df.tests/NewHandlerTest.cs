// --------------------------------------------------------------------------------
// <copyright file="NewHandlerTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Tests
{
    using Df.Options;
    using Xunit;
    using Xunit.Abstractions;

    public sealed class NewHandlerTest
        : OptionsHandlerTest<NewOptions>
    {
        public NewHandlerTest(ITestOutputHelper output, DfFixture fixture)
            : base(output, fixture)
        {
        }

        [Fact]
        [TemporaryFiles(Extension = ".json")]
        public void NewProjectTest()
        {
            var fileName = Temporary.GetTempFilePath();
            CreateProjectFile(fileName);
        }
    }
}