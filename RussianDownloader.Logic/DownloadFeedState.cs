namespace RussianDownloader.Logic
{
    using System.IO;
    using System.Threading.Tasks;

    internal class DownloadFeedState
    {
        public Task<Stream> FeedResponse { get; set; }
    }
}