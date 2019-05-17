// --------------------------------------------------------------------------------
// <copyright file="ProjectManagerTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Tests
{
    using System.IO;
    using Xunit;
    using Xunit.Abstractions;

    public sealed class ProjectManagerTest
        : IoTestBase
    {
        public ProjectManagerTest(ITestOutputHelper output, IoFixture fixture)
            : base(output, fixture)
        {
        }

        [Fact]
        [TemporaryFiles]
        public void LoadProject()
        {
            var project1 = CreateProject();
            var path1 = Temporary.GetTempFilePath();
            ProjectManager.SaveToFile(project1, path1);

            var project2 = ProjectManager.LoadFromFile(path1);
            IoAssert.AsExpected(project2);

            var path2 = Temporary.GetTempFilePath();
            ProjectManager.SaveToFile(project2, path2);

            var str1 = File.ReadAllText(path1);
            var str2 = File.ReadAllText(path2);
            Assert.Equal(str1, str2);
        }

        [Fact]
        [TemporaryFiles]
        public void SaveProject()
        {
            var project = CreateProject();
            var path = Temporary.GetTempFilePath();
            try
            {
                ProjectManager.SaveToFile(project, path);
                Output.WriteLine(File.ReadAllText(path));
            }
            finally
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}