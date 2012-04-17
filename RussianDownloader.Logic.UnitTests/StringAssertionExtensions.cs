namespace RussianDownloader.Logic.UnitTests
{
    using System;
    using System.Text;

    using FluentAssertions;
    using FluentAssertions.Assertions;

    public static class StringAssertionExtensions
    {
        public static void BeBase64DecodedTo(this StringAssertions assertions, string expected)
        {
            assertions.BeBase64DecodedTo(expected, String.Empty);
        }

        public static void BeBase64DecodedTo(this StringAssertions assertions, string expected, string reason, params object[] reasonArgs)
        {
            var decoded = Encoding.Default.GetString(Convert.FromBase64String(assertions.Subject));

            decoded.Should().Be(expected, reason, reasonArgs);
        }
    }
}