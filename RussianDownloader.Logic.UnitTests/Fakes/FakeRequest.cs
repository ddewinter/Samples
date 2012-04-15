namespace RussianDownloader.Logic.UnitTests.Fakes
{
    using System.Net;

    public class FakeRequest : WebRequest
    {
        private WebHeaderCollection _webHeaders = new WebHeaderCollection();

        public override WebHeaderCollection Headers
        {
            get
            {
                return _webHeaders;
            }
            set
            {
                _webHeaders = value;
            }
        }
    }
}