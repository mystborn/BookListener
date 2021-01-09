using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListener
{
    public class Book
    {
        public string Title { get; }
        public string Publisher { get; }
        public string Series { get; }
        public IReadOnlyList<string> Authors { get; }
        public IReadOnlyList<string> Tags { get; }
        public DateTime? Published { get; }
        public IReadOnlyList<string> Paragraphs { get; }

        public Book(string title,
                    string publisher,
                    string series,
                    IEnumerable<string> authors,
                    IEnumerable<string> tags,
                    DateTime? published,
                    IEnumerable<string> paragraphs)
        {
            Title = title;
            Publisher = publisher;
            Series = series;
            Authors = new List<string>(authors);
            Tags = new List<string>(tags);
            Published = published;
            Paragraphs = new List<string>(paragraphs);
        }
    }
}
