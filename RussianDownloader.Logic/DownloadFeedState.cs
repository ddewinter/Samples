namespace RussianDownloader.Logic
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    internal class DownloadFeedState
    {
        public Task<Stream> FeedResponse { get; set; }

        public IEnumerable<XElement> Elements { get; set; }
    }
}