namespace RussianDownloader.Logic.UnitTests
{
    using System;
    using System.Text;

    using FluentAssertions;
    using FluentAssertions.Assertions;

    public static class StringAssertionExtensions
    {
        public static void BeBasicAuthorizationHeader(this StringAssertions assertions, string expectedUserName, string expectedPassword)
        {
            assertions.BeBasicAuthorizationHeader(expectedUserName, expectedPassword, String.Empty);
        }

        public static void BeBasicAuthorizationHeader(this StringAssertions assertions, string expectedUserName, string expectedPassword, string reason, params object[] reasonArgs)
        {
            var startText = "Basic ";
            assertions.Subject.Should().StartWith(startText);

            var expected = string.Format("{0}:{1}", expectedUserName, expectedPassword);
            var decoded = Encoding.Default.GetString(Convert.FromBase64String(assertions.Subject.Substring(startText.Length)));
            decoded.Should().Be(expected, reason, reasonArgs);
        }
    }
}