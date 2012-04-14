namespace RussianDownloader.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    using RussianDownloader.Logic.Simulators;

    using FluentAssertions;

    [TestFixture]
    public class _1_DownloadFeed
    {
        [Test]
        public void UrlResourceAccessor_should_request_uri()
        {
            var subjectUnderTest = new UrlResourceAccessor();

            var stream = subjectUnderTest.GetResourceStream(new Location("http://www.example.com"));

            var result = new StreamReader(stream.Result).ReadToEnd();

            result.Should().Contain("iana");
        }
    }
}
