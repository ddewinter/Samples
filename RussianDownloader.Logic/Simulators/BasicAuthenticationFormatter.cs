namespace RussianDownloader.Logic.Simulators
{
    using System;
    using System.Text;

    public static class BasicAuthenticationFormatter
    {
        public static string FormatHeader(string userName, string password)
        {
            var authInfo = userName + ":" + password;
            return Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
        }
    }
}