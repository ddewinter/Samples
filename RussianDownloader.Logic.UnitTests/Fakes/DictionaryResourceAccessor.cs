namespace RussianDownloader.Logic.UnitTests.Fakes
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading.Tasks;

    using RussianDownloader.Logic.Simulators;

    internal class DictionaryResourceAccessor : IResourceAccessor
    {
        private readonly Dictionary<Location, Task<Stream>> _source;

        public DictionaryResourceAccessor(Dictionary<Location, Task<Stream>> source)
        {
            _source = source;
        }

        public Task<Stream> GetResourceStream(Location resourceLocation)
        {
            return _source[resourceLocation];
        }
    }
}