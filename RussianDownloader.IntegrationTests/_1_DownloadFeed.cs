namespace RussianDownloader.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    using RussianDownloader.Logic;
    using RussianDownloader.Logic.Simulators;

    using FluentAssertions;

    [TestFixture]
    public class _1_DownloadFeed
    {
        [Test]
        public void UrlResourceAccessor_should_request_uri_with_credentials()
        {
            var subjectUnderTest = new UrlResourceAccessor();

            var stream =
                subjectUnderTest.GetResourceStream(
                    new Location(
                        "http://www.russianpod101.com/premium_feed/feed.xml",
                        new Credentials("daviddewinter3985", "Moonraker003")));

            using (var streamReader = new StreamReader(stream.Result))
            {
                var line = streamReader.ReadLine();

                line.Should().Contain("<?xml");
            }
        }
    }
}
