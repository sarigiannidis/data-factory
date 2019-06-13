namespace Df.Stochastic.Fare.Tests
{
    using System;
    using System.Linq;
    using Xunit;
    using Xunit.Abstractions;

    public sealed class XegerTests
    {
        private readonly ITestOutputHelper _TestOutput;

        public static TheoryData<string> RegexPatternTestCases => new TheoryData<string>
        {
            "[ab]{4,6}",
            "[ab]{4,6}c",
            "(a|b)*ab",
            "[A-Za-z0-9]",
            "[A-Za-z0-9_]",
            "[A-Za-z]",
            "[ \t]",
            @"[(?<=\W)(?=\w)|(?<=\w)(?=\W)]",
            "[\x00-\x1F\x7F]",
            "[0-9]",
            "[^0-9]",
            "[\x21-\x7E]",
            "[a-z]",
            "[\x20-\x7E]",
            "[ \t\r\n\v\f]",
            "[^ \t\r\n\v\f]",
            "[A-Z]",
            "[A-Fa-f0-9]",
            "in[du]",
            "x[0-9A-Z]",
            "[^A-M]in",
            ".gr",
            @"\(.*l",
            "W*in",
            "[xX][0-9a-z]",
            @"\(\(\(ab\)*c\)*d\)\(ef\)*\(gh\)\{2\}\(ij\)*\(kl\)*\(mn\)*\(op\)*\(qr\)*",
            @"((mailto\:|(news|(ht|f)tp(s?))\://){1}\S+)",
            @"^http\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(/\S*)?$",
            @"^([1-zA-Z0-1@.\s]{1,255})$",
            "[A-Z][0-9A-Z]{10}",
            "[A-Z][A-Za-z0-9]{10}",
            "[A-Za-z0-9]{11}",
            "[A-Za-z]{11}",
            @"^[a-zA-Z''-'\s]{1,40}$",
            @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$",
            "a[a-z]",
            "[1-9][0-9]",
            @"\d{8}",
            @"\d{5}(-\d{4})?",
            @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}",
            @"\D{8}",
            @"\D{5}(-\D{4})?",
            @"\D{1,3}\.\D{1,3}\.\D{1,3}\.\D{1,3}",
            "^(?:[a-z0-9])+$",
            "^(?i:[a-z0-9])+$",
            "^(?s:[a-z0-9])+$",
            "^(?m:[a-z0-9])+$",
            "^(?n:[a-z0-9])+$",
            "^(?x:[a-z0-9])+$",
            "\\S+.*",
            @"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$",
            @"^\s1\s+2\s3\s?4\s*$",
            @"(\s123)+",
            @"\Sabc\S{3}111",
            @"^\S\S  (\S)+$",
            @"\\abc\\d",
            @"\w+1\w{4}",
            @"\W+1\w?2\W{4}",
            "^[^$]$",
        };

        public XegerTests(ITestOutputHelper testOutput) => _TestOutput = testOutput;

        [Theory]
        [MemberData(nameof(RegexPatternTestCases))]
        public void GeneratedTextIsCorrect(string pattern)
        {
            // Arrange
            const int repeatCount = 3;

            var randomSeed = Environment.TickCount;
            _TestOutput.WriteLine($"Random seed: {randomSeed}");

            using var random = new HardRandom();

            var sut = new Xeger(pattern, random);

            // Act
            var result = Enumerable.Repeat(0, repeatCount)
                .Select(_ =>
                {
                    var generatedValue = sut.Generate();
                    _TestOutput.WriteLine($"Generated value: {generatedValue}");
                    return generatedValue;
                })
                .ToArray();

            // Assert
            Assert.All(result, _ => Assert.Matches(pattern, _));
        }
    }
}