namespace RussianDownloader.Logic.Simulators
{
    using System.Net;

    public class Location
    {
        public Location(string locationUri, NetworkCredential credentials)
        {
            LocationUri = locationUri;
            Credentials = credentials;
        }

        public Location(string locationUri) :
            this(locationUri, null)
        {
        }

        public string LocationUri { get; set; }

        public NetworkCredential Credentials { get; set; }
    }
}