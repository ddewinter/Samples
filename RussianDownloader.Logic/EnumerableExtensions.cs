using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RussianDownloader.Logic
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Lesson> ParseLessons(this IEnumerable<XElement> elements)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Lesson> DiscardIfAlreadyDownloaded(this IEnumerable<Lesson> lessons)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Lesson> SaveLessonContentToDisk(this IEnumerable<Lesson> lessons )
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Lesson> PersistLessons(this IEnumerable<Lesson> lessons)
        {
            throw new NotImplementedException();
        }
    }
}