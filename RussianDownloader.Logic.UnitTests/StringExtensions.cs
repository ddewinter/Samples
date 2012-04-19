namespace RussianDownloader.Logic.UnitTests
{
    using System.Text;

    public static class StringExtensions
    {
         public static byte[] AsBytes(this string input)
         {
             return Encoding.Default.GetBytes(input);
         }
    }
}