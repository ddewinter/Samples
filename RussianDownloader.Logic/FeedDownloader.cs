namespace RussianDownloader.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    using RussianDownloader.Logic.Simulators;

    public class FeedDownloader
    {
        internal const string PremiumFeedUrl = "http://www.russianpod101.com/premium_feed/feed.xml";

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

        public IEnumerable<XElement> DownloadFeed()
        {
            throw new NotImplementedException();
        }

        internal static DownloadFeedState ConvertStreamToXml(DownloadFeedState state)
        {
            throw new NotImplementedException();
        }

        internal DownloadFeedState IssueWebRequest(DownloadFeedState state)
        {
            state.FeedResponse = _resourceAccessor.GetResourceStream(PremiumFeedUrl);

            return state;
        }
    }
}