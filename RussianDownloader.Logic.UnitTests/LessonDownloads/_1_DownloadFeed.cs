using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RussianDownloader.Logic.UnitTests.LessonDownloads
{
    using FluentAssertions;

    [TestFixture]
    public class _1_DownloadFeed
    {
        [Test]
        public void DownloadFeedSequence_should_issue_web_request_then_convert_to_XElement()
        {
            // Arrange
            var subjectUnderTest = new FeedDownloader();

            // Assert
            subjectUnderTest.DownloadFeedSequence.Should().Equal(
                new Func<DownloadFeedState, DownloadFeedState>[]
                {
                    FeedDownloader.IssueWebRequest,
                    FeedDownloader.ConvertStreamToXml
                });
        }
    }
}
