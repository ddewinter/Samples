namespace RussianDownloader.Logic.Simulators
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IResourceAccessor
    {
        Task<Stream> GetResourceStream(string resourceLocation);
    }
}