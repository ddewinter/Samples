namespace RussianDownloader.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    internal class PodcastFeedReader
    {
        public IEnumerable<XElement> GetPodcastItems(Stream stream)
        {
            throw new NotImplementedException();
        }

        internal XmlReader LoadXmlReader(Stream stream)
        {
            return XmlReader.Create(stream);
        }

        public IEnumerable<XElement> ExtractPodcastItemElements(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.LocalName == "item")
                {
                    yield return (XElement)XNode.ReadFrom(reader);
                }
            }
        }
    }
}