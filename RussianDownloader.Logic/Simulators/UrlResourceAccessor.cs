namespace RussianDownloader.Logic.Simulators
{
    using System.IO;
    using System.Threading.Tasks;

    public class UrlResourceAccessor : IResourceAccessor
    {
        public Task<Stream> GetResourceStream(string resourceLocation)
        {
            throw new System.NotImplementedException();
        }
    }
}