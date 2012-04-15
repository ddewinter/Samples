namespace RussianDownloader.Logic.Simulators
{
    using System;
    using System.Net;
    using System.Text;

    public static class BasicAuthenticationFormatter
    {
        public static string FormatHeader(NetworkCredential credentials)
        {
            var authInfo = credentials.UserName + ":" + credentials.Password;
            return Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
        }
    }
}