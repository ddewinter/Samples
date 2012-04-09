using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RussianDownloader.Logic.UnitTests.LessonDownloads
{
    using System.IO;
    using System.Threading.Tasks;

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

        [Test]
        public void IssueWebRequest_should_populate_FeedResponse()
        {
            // Arrange
            var subjectUnderTest = new FeedDownloader();
            var state = new DownloadFeedState();

            // Act
            var resultState = FeedDownloader.IssueWebRequest(state);

            // Assert
            state.FeedResponse.Should().NotBeNull();
        }
    }
}
