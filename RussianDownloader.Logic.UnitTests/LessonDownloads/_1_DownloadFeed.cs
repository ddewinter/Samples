﻿namespace RussianDownloader.Logic.UnitTests.LessonDownloads
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    using FluentAssertions;

    using NUnit.Framework;

    using RussianDownloader.Logic.Simulators;
    using RussianDownloader.Logic.UnitTests.Fakes;

    using System.Linq;

    [TestFixture]
    public class _1_DownloadFeed
    {
        private static Task<Stream> EmptyResource = Task<Stream>.Factory.StartNew(state => null, null);

        private static Dictionary<Location, Task<Stream>> _resources = new Dictionary<Location, Task<Stream>>
            {
                {
                    new Location(FeedDownloader.PremiumFeedUrl, SiteCredentials.RussianCredentials),
                    EmptyResource
                }
            };

        private static readonly DictionaryResourceAccessor DictionaryResourceAccessor = new DictionaryResourceAccessor(_resources);

        private readonly DownloadFeedState _downloadFeedState = new DownloadFeedState();

        private readonly FeedDownloader _feedDownloader = new FeedDownloader(DictionaryResourceAccessor);

        [Test]
        public void DownloadFeedSequence_should_issue_web_request_then_convert_to_XElement()
        {
            // Arrange
            FeedDownloader subjectUnderTest = _feedDownloader;

            // Assert
            subjectUnderTest.DownloadFeedSequence.Should().Equal(
                new Func<DownloadFeedState, DownloadFeedState>[] { subjectUnderTest.IssueWebRequest, FeedDownloader.ConvertStreamToXml });
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
            var credentials = new Credentials("UserName", "Password");

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
            var credentials = new Credentials(userName, password);

            // Act
            var header = BasicAuthenticationFormatter.FormatHeader(credentials);

            // Assert
            header.Should().BeBasicAuthorizationHeader(userName, password);
        }

        [Test]
        public void BasicAuthenticationFormatter_should_throw_if_credentials_are_null()
        {
            // Act
            Action a = () => BasicAuthenticationFormatter.FormatHeader(null);

            // Assert
            a.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("credentials");
        }

        [Test]
        public void BasicAuthenticationFormatter_should_throw_if_user_name_contains_colon()
        {
            // Arrange
            var credentials = new Credentials("User:Name", "Password");

            // Act
            Action a = () => BasicAuthenticationFormatter.FormatHeader(credentials);

            // Assert
            a.ShouldThrow<ArgumentException>().And.ParamName.Should().Be("credentials");
        }

        [Test]
        public void New_Credentials_sets_user_name()
        {
            // Arrange
            var userName = "UserName";
            var password = "Password";

            // Act
            var subjectUnderTest = new Credentials(userName, password);

            // Assert
            subjectUnderTest.UserName.Should().Be(userName);
        }

        [Test]
        public void New_Credentials_sets_password()
        {
            // Arrange
            var userName = "UserName";
            var password = "Password";

            // Act
            var subjectUnderTest = new Credentials(userName, password);

            // Assert
            subjectUnderTest.Password.Should().Be(password);
        }

        [Test]
        public void AddBasicAuthenticationHeader_should_not_add_header_without_credentials()
        {
            // Arrange
            var fakeRequest = new FakeRequest();

            // Act
            UrlResourceAccessor.AddBasicAuthenticationHeader(fakeRequest, null);

            // Assert
            fakeRequest.Headers.Should().HaveCount(0);
        }

        [Test]
        public void AddBasicAuthenticationHeader_should_add_Authorization_header_based_on_credentials()
        {
            // Arrange
            var userName = "David";
            var password = "P4ssw0rd";
            var credentials = new Credentials(userName, password);
            var fakeRequest = new FakeRequest();

            // Act
            UrlResourceAccessor.AddBasicAuthenticationHeader(fakeRequest, credentials);

            // Assert
            fakeRequest.Headers.Keys.Should().Contain(HttpRequestHeader.Authorization.ToString());

            var header = fakeRequest.Headers[HttpRequestHeader.Authorization.ToString()];
            header.Should().BeBasicAuthorizationHeader(userName, password);
        }

        [Test]
        public void AddCustomHeaders_should_add_headers_based_on_custom_options()
        {
            // Arrange
            var fakeRequest = new FakeRequest();
            var options = new ResourceAccessorOptions();
            var header = "Foo";
            var headerValue = "Bar";
            options[header] = headerValue;

            // Act
            UrlResourceAccessor.AddCustomHeaders(fakeRequest, options);

            // Assert
            fakeRequest.Headers.Keys.Should().Contain(header);
            fakeRequest.Headers[header].Should().Be(headerValue);
        }

        [Test]
        public void AddCustomHeaders_should_not_add_headers_if_options_are_null()
        {
            // Arrange
            var fakeRequest = new FakeRequest();

            // Act
            UrlResourceAccessor.AddCustomHeaders(fakeRequest, null);

            // Assert
            fakeRequest.Headers.Should().HaveCount(0);
        }

        [Test]
        public void ResourceAccessorOptions_indexer_should_return_custom_options()
        {
            // Arrange
            var subjectUnderTest = new ResourceAccessorOptions();
            var header = "Foo";
            var headerValue = "Bar";
            subjectUnderTest[header] = headerValue;

            // Act
            var actual = subjectUnderTest[header];

            // Assert
            actual.Should().Be(headerValue);
        }

        // TODO: Is this test necessary?
        [Test]
        public void IssueWebRequest_should_request_resource_stream_with_iTunes_user_agent()
        {
            // Arrange
            var subjectUnderTest = new FeedDownloader(new ITunesOnlyDictionaryResourceAccessor(_resources));
            var state = _downloadFeedState;

            // Act
            var response = subjectUnderTest.IssueWebRequest(state);

            // Assert
            response.FeedResponse.Should().Be(EmptyResource);
        }

        [Test]
        public void CreateResourceAccessorOptions_should_create_ResourceAccessorOptions_with_iTunes_user_agent()
        {
            // Arrange, Act
            var options = FeedDownloader.CreateResourceAccessorOptions();

            // Assert
            options[FeedDownloader.UserAgentOptionName].Should().Be(FeedDownloader.ITunesUserAgent);
        }

        // TODO: Since this test just verifies that steps are stitched together, is this a valuable test?
        [Test]
        public void PodcastFeedReader_should_expose_podcast_items_as_XElements()
        {
            // Arrange
            var subjectUnderTest = new PodcastFeedReader();
            var stream =
                new MemoryStream(@"<?xml version=""1.0""?><rss><channel><item>text</item><item></item></channel></rss>".AsBytes());

            // Act
            var items = subjectUnderTest.GetPodcastItems(stream);

            // Assert
            items.Should().HaveCount(2);
        }

        [Test]
        public void PodcastFeedReader_should_convert_empty_stream_to_XmlReader()
        {
            // Arrange
            var subjectUnderTest = new PodcastFeedReader();
            var stream = new MemoryStream();

            // Act
            var reader = PodcastFeedReader.LoadXmlReader(stream);

            // Assert
            Action a = () => reader.Read();
            a.ShouldThrow<XmlException>();
        }

        [Test]
        public void PodcastFeedReader_should_convert_input_stream_to_XmlReader()
        {
            // Arrange
            var subjectUnderTest = new PodcastFeedReader();
            var stream = new MemoryStream(@"<?xml version=""1.0""?>".AsBytes());

            // Act
            var reader = PodcastFeedReader.LoadXmlReader(stream);

            // Assert
            reader.Read();

            reader.NodeType.Should().Be(XmlNodeType.XmlDeclaration);
        }

        // TODO: Are these two tests no longer unit test because they take too long and/or interact with XML parsing? How can this improve?
        [Test]
        public void PodcastFeedReader_should_read_item_from_feed()
        {
            // Arrange
            var reader =
                XmlReader.Create(
                    new MemoryStream(@"<?xml version=""1.0""?><rss><channel><item>text</item></channel></rss>".AsBytes()));

            // Act
            var elements = PodcastFeedReader.ExtractPodcastItemElements(reader).ToArray();

            // Assert
            elements.Should().HaveCount(1).And.Contain(elem => elem.Value == "text");
        }

        [Test]
        public void PodcastFeedReader_should_read_multiple_items_from_feed()
        {
            // Arrange
            var reader =
                XmlReader.Create(
                    new MemoryStream(@"<?xml version=""1.0""?><rss><channel><item>text</item><item>as</item></channel></rss>".AsBytes()));

            // Act
            var elements = PodcastFeedReader.ExtractPodcastItemElements(reader).ToArray();

            // Assert
            elements.Should().HaveCount(2).And.Contain(elem => elem.Value == "as");
        }

        [Test]
        public void ConvertStreamToXml_should_convert_FeedResponse()
        {
            // Arrange
            var state = new DownloadFeedState
                {
                    FeedResponse =
                        Task<Stream>.Factory.StartNew(
                            () =>
                            new MemoryStream(
                                @"<?xml version=""1.0""?><rss><channel><item>first</item></channel></rss>".AsBytes()))
                };

            // Act
            var resultState = FeedDownloader.ConvertStreamToXml(state);

            // Assert
            resultState.Elements.Should().HaveCount(1);
        }
    }
}