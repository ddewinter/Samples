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
            return ExtractPodcastItemElements(LoadXmlReader(stream));
        }

        internal static XmlReader LoadXmlReader(Stream stream)
        {
            return XmlReader.Create(stream);
        }

        internal static IEnumerable<XElement> ExtractPodcastItemElements(XmlReader reader)
        {
            while (reader.Read())
            {
                while (reader.NodeType == XmlNodeType.Element && reader.LocalName == "item")
                {
                    yield return (XElement)XNode.ReadFrom(reader);
                }
            }
        }
    }
}