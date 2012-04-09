using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RussianDownloader.Logic
{
    using System.IO;
    using System.Threading.Tasks;

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
            state.FeedResponse = Task<Stream>.Factory.StartNew(taskState => null, null);

            return state;
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