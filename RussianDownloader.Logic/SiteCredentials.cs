namespace RussianDownloader.Logic
{
    using System.Net;

    public class SiteCredentials
    {
        private static Credentials _russianCredentials = new Credentials("daviddewinter3985", "Fr4M3w0RK024");

        public static Credentials RussianCredentials
        {
            get
            {
                return _russianCredentials;
            }
        }
    }
}