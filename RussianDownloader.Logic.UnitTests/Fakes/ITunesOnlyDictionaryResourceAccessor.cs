namespace RussianDownloader.Logic.UnitTests.Fakes
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using RussianDownloader.Logic.Simulators;

    internal class ITunesOnlyDictionaryResourceAccessor : DictionaryResourceAccessor
    {
        public ITunesOnlyDictionaryResourceAccessor(Dictionary<Location, Task<Stream>> source)
            : base(source)
        {
        }

        public override Task<Stream> GetResourceStream(Location resourceLocation, ResourceAccessorOptions options)
        {
            if (options != null && options[FeedDownloader.UserAgentOptionName] == FeedDownloader.ITunesUserAgent)
            {
                return base.GetResourceStream(resourceLocation, options);
            }

            return null;
        }
    }
}