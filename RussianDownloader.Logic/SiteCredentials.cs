namespace RussianDownloader.Logic
{
    using System.Net;

    public class SiteCredentials
    {
        private static Credentials _russianCredentials = new Credentials("daviddewinter3985", "Moonraker003");

        public static Credentials RussianCredentials
        {
            get
            {
                return _russianCredentials;
            }
        }
    }
}