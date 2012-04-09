namespace RussianDownloader.Logic.UnitTests.LessonDownloads
{
    using System;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class _1_DownloadFeed
    {
        private readonly FeedDownloader _feedDownloader = new FeedDownloader();

        private DownloadFeedState _downloadFeedState = new DownloadFeedState();

        [Test]
        public void DownloadFeedSequence_should_issue_web_request_then_convert_to_XElement()
        {
            // Arrange
            FeedDownloader subjectUnderTest = _feedDownloader;

            // Assert
            subjectUnderTest.DownloadFeedSequence.Should().Equal(
                new Func<DownloadFeedState, DownloadFeedState>[]
                    { FeedDownloader.IssueWebRequest, FeedDownloader.ConvertStreamToXml });
        }

        [Test]
        public void IssueWebRequest_should_return_input_state()
        {
            // Arrange
            var state = _downloadFeedState;

            // Act
            DownloadFeedState resultState = FeedDownloader.IssueWebRequest(state);

            // Assert
            resultState.Should().Be(state);
        }

        [Test]
        public void IssueWebRequest_should_populate_FeedResponse()
        {
            // Arrange
            var state = _downloadFeedState;

            // Act
            FeedDownloader.IssueWebRequest(state);

            // Assert
            state.FeedResponse.Should().NotBeNull();
        }
    }
}