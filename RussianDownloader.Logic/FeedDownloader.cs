using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RussianDownloader.Logic
{
    public class FeedDownloader
    {
        private readonly Func<DownloadFeedState, DownloadFeedState>[] _downloadFeedSequence =
            new Func<DownloadFeedState, DownloadFeedState>[]
            {
                IssueWebRequest,
                ConvertStreamToXml
            };

        internal Func<DownloadFeedState, DownloadFeedState>[] DownloadFeedSequence
        {
            get { return _downloadFeedSequence; }
        }

        internal static DownloadFeedState IssueWebRequest(DownloadFeedState state)
        {
            throw new NotImplementedException();
        }

        internal static DownloadFeedState ConvertStreamToXml(DownloadFeedState state)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<XElement> DownloadFeed()
        {
            throw new NotImplementedException();
        }
    }
}