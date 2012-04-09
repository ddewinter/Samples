namespace RussianDownloader.Logic.UnitTests.Fakes
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading.Tasks;

    using RussianDownloader.Logic.Simulators;

    internal class DictionaryResourceAccessor : IResourceAccessor
    {
        private readonly Dictionary<string, Task<Stream>> _source;

        public DictionaryResourceAccessor(Dictionary<string, Task<Stream>> source)
        {
            _source = source;
        }

        public Task<Stream> GetResourceStream(string resourceLocation)
        {
            return _source[resourceLocation];
        }
    }
}