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
        private Book _book;

        public StringExtractor(string text, 
                               Regex paragraphSeparator,
                               string title = "",
                               string publisher = "",
                               string series = "",
                               IEnumerable<string> authors = null,
                               IEnumerable<string> tags = null,
                               DateTime? published = null)
        {
            var paragraphs = paragraphSeparator.Split(text);
            _book = new Book(title, publisher, series, authors, tags, published, paragraphs);
        }

        public Book ExtractBook()
        {
            return _book;
        }
    }
}
