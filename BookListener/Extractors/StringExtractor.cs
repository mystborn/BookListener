using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookListener.Extractors
{
    public class StringExtractor : ITextExtractor
    {
        private BookInfo _info;
        private Book _book;

        public StringExtractor(string text,
                               Regex paragraphSeparator,
                               string title = "",
                               string publisher = "",
                               string series = "",
                               IEnumerable<string> authors = null,
                               IEnumerable<string> tags = null,
                               DateTime? published = null,
                               IEnumerable<Chapter> chapters = null)
        {
            _info = new BookInfo()
            {
                Title = title,
                Publisher = publisher,
                Series = series,
                Authors = authors?.ToList(),
                Tags = tags?.ToList(),
                Published = published,
                Chapters = chapters?.ToList()
            };

            var paragraphs = paragraphSeparator.Split(text);
            _book = new Book(_info, paragraphs.Where(s => !string.IsNullOrWhiteSpace(s)));
        }

        public Task<BookInfo> GetInfoAsync()
        {
            return Task.FromResult(_info);
        }

        public Task<Book> GetBookAsync()
        {
            return Task.FromResult(_book);
        }
    }
}
