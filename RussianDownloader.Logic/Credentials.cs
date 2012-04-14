namespace RussianDownloader.Logic
{
    using System.Net;

    public class Credentials
    {
        private static NetworkCredential _russianCredentials = new NetworkCredential("daviddewinter3985", "Fr4M3w0RK024");

        public static NetworkCredential RussianCredentials
        {
            get
            {
                return _russianCredentials;
            }
        }
    }
}