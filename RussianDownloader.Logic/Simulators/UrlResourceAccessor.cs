namespace RussianDownloader.Logic.Simulators
{
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    public class UrlResourceAccessor : IResourceAccessor
    {
        public Task<Stream> GetResourceStream(Location resourceLocation)
        {
            var request = WebRequest.Create(resourceLocation.LocationUri);

            return Task<Stream>.Factory.FromAsync(
                request.BeginGetResponse,
                (result) => request.EndGetResponse(result).GetResponseStream(),
                null);
        }
    }
}