namespace RussianDownloader.Logic.UnitTests.LessonDownloads
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using FluentAssertions;

    using NUnit.Framework;

    using RussianDownloader.Logic.Simulators;
    using RussianDownloader.Logic.UnitTests.Fakes;

    [TestFixture]
    public class _1_DownloadFeed
    {
        private static Task<Stream> EmptyResource = Task<Stream>.Factory.StartNew(state => null, null);

        private static readonly DictionaryResourceAccessor DictionaryResourceAccessor =
            new DictionaryResourceAccessor(
                new Dictionary<Location, Task<Stream>>
                    {
                        {
                            new Location(FeedDownloader.PremiumFeedUrl, Credentials.RussianCredentials),
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
            FeedDownloader subjectUnderTest = _feedDownloader;
            DownloadFeedState state = _downloadFeedState;

            // Act
            DownloadFeedState resultState = subjectUnderTest.IssueWebRequest(state);

            // Assert
            resultState.Should().Be(state);
        }

        [Test]
        public void IssueWebRequest_should_invoke_resource_accessor_with_correct_url_and_credentials()
        {
            // Arrange
            FeedDownloader subjectUnderTest = _feedDownloader;
            DownloadFeedState state = _downloadFeedState;

            // Act
            subjectUnderTest.IssueWebRequest(state);

            // Assert
            state.FeedResponse.Should().Be(EmptyResource);
        }

        [Test]
        public void New_Location_should_set_location_uri()
        {
            // Arrange
            var locationUri = "locationUri";

            // Act
            var subjectUnderTest = new Location(locationUri);

            // Assert
            subjectUnderTest.LocationUri.Should().Be(locationUri);
        }

        [Test]
        public void New_Location_should_set_credentials()
        {
            // Arrange
            var credentials = new NetworkCredential("UserName", "Password");

            // Act
            var subjectUnderTest = new Location("uri", credentials);

            // Assert
            subjectUnderTest.Credentials.Should().Be(credentials);
        }

        [Test]
        public void GetResourceStream_should_throw_when_location_is_null()
        {
            // Arrange
            var subjectUnderTest = new UrlResourceAccessor();

            // Act
            Action a = () => subjectUnderTest.GetResourceStream(null);

            // Assert
            a.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("resourceLocation");
        }

        [Test]
        public void BasicAuthenticationFormatter_should_base64_encode_user_name_and_password()
        {
            // Arrange
            var userName = "UserName";
            var password = "Password";
            var credentials = new NetworkCredential(userName, password);

            // Act
            var header = BasicAuthenticationFormatter.FormatHeader(credentials);

            // Assert
            var decoded = Encoding.Default.GetString(Convert.FromBase64String(header));
            decoded.Should().Be(string.Format("{0}:{1}", userName, password));
        }

        [Test]
        public void BasicAuthenticationFormatter_should_throw_if_credentials_are_null()
        {
            // Arrange
            Action a = () => BasicAuthenticationFormatter.FormatHeader(null);

            // Act, Assert
            a.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("credentials");
        }
    }
}