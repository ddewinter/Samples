namespace RussianDownloader.Logic.Simulators
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    public class UrlResourceAccessor : IResourceAccessor
    {
        public Task<Stream> GetResourceStream(Location resourceLocation)
        {
            return GetResourceStream(resourceLocation, null);
        }

        public Task<Stream> GetResourceStream(Location resourceLocation, ResourceAccessorOptions options)
        {
            if (resourceLocation == null)
            {
                throw new ArgumentNullException("resourceLocation");
            }

            var request = WebRequest.Create(resourceLocation.LocationUri);
            AddBasicAuthenticationHeader(request, resourceLocation.Credentials);
            AddCustomHeaders(request, options);

            return Task<Stream>.Factory.FromAsync(
                request.BeginGetResponse,
                (result) => request.EndGetResponse(result).GetResponseStream(),
                null);
        }

        internal static void AddBasicAuthenticationHeader(WebRequest webRequest, Credentials credentials)
        {
            if (credentials != null)
            {
                webRequest.Headers[HttpRequestHeader.Authorization] =
                    BasicAuthenticationFormatter.FormatHeader(credentials);
            }
        }

        internal static void AddCustomHeaders(WebRequest fakeRequest, ResourceAccessorOptions options)
        {
            if (options != null)
            {
                foreach (var option in options)
                {
                    fakeRequest.Headers[option.Key] = option.Value;
                }
            }
        }
    }
}