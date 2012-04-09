using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RussianDownloader.Logic;

namespace RussianDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            new FeedDownloader()
                .DownloadFeed()
                .ParseLessons()
                .DiscardIfAlreadyDownloaded()
                .SaveLessonContentToDisk()
                .PersistLessons();
        }
    }
}
