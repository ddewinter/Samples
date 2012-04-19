namespace RussianDownloader.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    using RussianDownloader.Logic.Simulators;

    using System.Linq;

    public class FeedDownloader
    {
        internal const string PremiumFeedUrl = "http://www.russianpod101.com/premium_feed/feed.xml";
        internal const string ITunesUserAgent = "iTunes/10.6.1 (Windows; Microsoft Windows 7 x64 Enterprise Edition Service Pack 1 (Build 7601)) AppleWebKit/534.54.16";
        internal const string UserAgentOptionName = "User-Agent";

        private readonly Func<DownloadFeedState, DownloadFeedState>[] _downloadFeedSequence;

        private readonly IResourceAccessor _resourceAccessor;

        public FeedDownloader()
            : this(new UrlResourceAccessor())
        {
        }

        internal FeedDownloader(IResourceAccessor resourceAccessor)
        {
            _resourceAccessor = resourceAccessor;
            _downloadFeedSequence = new Func<DownloadFeedState, DownloadFeedState>[]
                { IssueWebRequest, ConvertStreamToXml };
        }

        internal Func<DownloadFeedState, DownloadFeedState>[] DownloadFeedSequence
        {
            get
            {
                return _downloadFeedSequence;
            }
        }

        // TODO: Can this and should this be unit tested?
        public IEnumerable<XElement> DownloadFeed()
        {
            var state = new DownloadFeedState();
            _downloadFeedSequence.Aggregate(state, (current, op) => op(current));

            return state.Elements;
        }

        internal DownloadFeedState IssueWebRequest(DownloadFeedState state)
        {
            var options = CreateResourceAccessorOptions();

            state.FeedResponse = _resourceAccessor.GetResourceStream(
                new Location(PremiumFeedUrl, SiteCredentials.RussianCredentials),
                options);

            return state;
        }

        internal static DownloadFeedState ConvertStreamToXml(DownloadFeedState state)
        {
            state.Elements = new PodcastFeedReader().GetPodcastItems(state.FeedResponse.Result);
            
            return state;
        }

        internal static ResourceAccessorOptions CreateResourceAccessorOptions()
        {
            var options = new ResourceAccessorOptions();
            options[UserAgentOptionName] = ITunesUserAgent;
            return options;
        }
    }
}