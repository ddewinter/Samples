namespace RussianDownloader.Logic.UnitTests.LessonDownloads
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using FluentAssertions;

    using NUnit.Framework;

    using RussianDownloader.Logic.UnitTests.Fakes;

    [TestFixture]
    public class _1_DownloadFeed
    {
        private static Task<Stream> EmptyResource = Task<Stream>.Factory.StartNew(state => null, null);

        private static readonly DictionaryResourceAccessor DictionaryResourceAccessor =
            new DictionaryResourceAccessor(
                new Dictionary<string, Task<Stream>>
                    {
                        {
                            FeedDownloader.PremiumFeedUrl,
                            EmptyResource
                        }
                    });

        private readonly DownloadFeedState _downloadFeedState = new DownloadFeedState();

        private readonly FeedDownloader _feedDownloader = new FeedDownloader(DictionaryResourceAccessor);


        [Test]
        public void DownloadFeedSequence_should_issue_web_request_then_convert_to_XElement()
        {
            // Arrange
            FeedDownloader subjectUnderTest = _feedDownloader;

            // Assert
            subjectUnderTest.DownloadFeedSequence.Should().Equal(
                new Func<DownloadFeedState, DownloadFeedState>[]
                    { subjectUnderTest.IssueWebRequest, FeedDownloader.ConvertStreamToXml });
        }

        [Test]
        public void IssueWebRequest_should_return_input_state()
        {
            // Arrange
            DownloadFeedState state = _downloadFeedState;

            // Act
            DownloadFeedState resultState = _feedDownloader.IssueWebRequest(state);

            // Assert
            resultState.Should().Be(state);
        }

        [Test]
        public void IssueWebRequest_should_populate_FeedResponse()
        {
            // Arrange
            DownloadFeedState state = _downloadFeedState;

            // Act
            _feedDownloader.IssueWebRequest(state);

            // Assert
            state.FeedResponse.Should().NotBeNull();
        }

        [Test]
        public void IssueWebRequest_should_invoke_resource_accessor_with_correct_url()
        {
            // Arrange
            FeedDownloader subjectUnderTest = _feedDownloader;
            DownloadFeedState state = _downloadFeedState;

            // Act
            subjectUnderTest.IssueWebRequest(state);

            // Assert
            state.FeedResponse.Should().Be(EmptyResource);
        }
    }
}